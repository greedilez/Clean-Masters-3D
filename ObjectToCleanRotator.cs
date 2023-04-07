using UnityEngine;

public class ObjectToCleanRotator : MonoBehaviour
{
    [SerializeField] private ObjectToCleanSpawner _objectToCleanSpawner;

    [SerializeField] private float _rotationSpeed = 1f;

    private void Update() => RotateTargetToCleanObjectByFinger();

    private void RotateTargetToCleanObjectByFinger() {
        if(Input.touchCount > 0 && AtMiddlePositionObjectHandler.IsTargetObjectAtMiddlePosition) {
            if(ObjectCleanHandler.Instance.CurrentSpongeState == SpongeState.Disabled) {
                Touch touch = Input.GetTouch(0);
                _objectToCleanSpawner.CurrentObjectAtCleaning.transform.Rotate(new Vector3(0, (-touch.deltaPosition.x * _rotationSpeed), 0), Space.Self);
            }
        }
    }
}
