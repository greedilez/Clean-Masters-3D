using UnityEngine;
using UnityEngine.Events;

public class ObjectCleanHandler : MonoBehaviour
{
    [SerializeField] private GameObject _sponge;

    [SerializeField] private Animator _spongeAnimator;

    [SerializeField] private Transform _spongeDisabledTransform, _spongeIdleTransform;

    public UnityEvent OnTookTheSponge, OnLeftTheSponge;

    [SerializeField] private SpongeState _currentSpongeState = SpongeState.Disabled;

    public SpongeState CurrentSpongeState{ get => _currentSpongeState; }

    public static ObjectCleanHandler Instance;

    private const string _objectToCleanTag = "ObjectToClean";

    private const string _dirtTag = "DirtBlock";

    private RaycastHit _raycastHit;

    [SerializeField] private DirtBlock _targetDirtBlock = null;

    private void Awake() => Instance = this;

    private void FixedUpdate() {
        TrackForAttemptToClean();
        CleanDirtBlocks();
    }

    private void TrackForAttemptToClean() {
        if(_currentSpongeState != SpongeState.Disabled) {
            if(Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                    SendRaycastFromTouch(touch);
                }
                else _currentSpongeState = SpongeState.Idle;
            }
            else _currentSpongeState = SpongeState.Idle;
        }
    }
    
    private void SendRaycastFromTouch(Touch touch) {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        float maxDistance = 10f;
        if(Physics.Raycast(ray, out hit, maxDistance)) {
            if (hit.collider.tag == _objectToCleanTag || hit.collider.tag == _dirtTag) {
                _currentSpongeState = SpongeState.Cleaning;
                _raycastHit = hit;
            }
            else _currentSpongeState = SpongeState.Idle;
        }
        else _currentSpongeState = SpongeState.Idle;
    }

    private void Update() {
        MoveTheSpongeDependingOnState();
        EnableSpongeCleaningAnimationStateAtRightTime();
    }

    public void TryToTakeSponge() {
        if (_currentSpongeState == SpongeState.Disabled) {
            _currentSpongeState = SpongeState.Idle;
            OnTookTheSponge.Invoke();
        }
        else LeaveTheSponge();
    }

    private void CleanDirtBlocks() {
        float cleaningSpeed = 0.5f;
        if(_currentSpongeState == SpongeState.Cleaning) {
            if(_raycastHit.collider.tag == _dirtTag) {
                UpdateTargetDirtBlock();
                _targetDirtBlock.DecreaseCleaningLeftValue(cleaningSpeed);
            }
        }
    }

    private void UpdateTargetDirtBlock() {
        if (_targetDirtBlock == null || _raycastHit.collider.GetComponent<DirtBlock>() != _targetDirtBlock) {
            _targetDirtBlock = _raycastHit.collider.GetComponent<DirtBlock>();
        }
    }

    private void LeaveTheSponge() {
        _currentSpongeState = SpongeState.Disabled;
        OnLeftTheSponge.Invoke();
    }

    private void MoveTheSpongeDependingOnState() {
        Vector3 targetPosition = Vector3.zero;
        float maxDistanceDelta = 5.5f;
        switch (_currentSpongeState) {
            case SpongeState.Disabled:
                targetPosition = _spongeDisabledTransform.position;
                break;

            case SpongeState.Idle:
                targetPosition = _spongeIdleTransform.position;
                break;

            case SpongeState.Cleaning:
                targetPosition = _raycastHit.point + (_raycastHit.normal * 0.0001f);
                break;
        }
        _sponge.transform.position = Vector3.MoveTowards(_sponge.transform.position, targetPosition, (maxDistanceDelta * Time.deltaTime));

        if (CurrentSpongeState == SpongeState.Cleaning) _sponge.transform.rotation = Quaternion.FromToRotation(Vector3.up, _raycastHit.normal);
        else _sponge.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    private void EnableSpongeCleaningAnimationStateAtRightTime() {
        if (CurrentSpongeState == SpongeState.Cleaning) {
            try {
                _spongeAnimator.SetBool("Cleaning", (_raycastHit.collider.tag == _dirtTag));
            }
            catch { }
        }
        else _spongeAnimator.SetBool("Cleaning", false);
    }
}
public enum SpongeState{ Disabled, Idle, Cleaning }