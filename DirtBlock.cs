using UnityEngine;

public class DirtBlock : MonoBehaviour
{
    [SerializeField] private float _cleaningLeft = 1f;

    public float CleaningLeft{ get => _cleaningLeft; set => _cleaningLeft = value; }

    public void DecreaseCleaningLeftValue(float speed) {
        float decreasingValue = (speed * Time.deltaTime);
        _cleaningLeft -= decreasingValue;
        DecreaseScale(decreasingValue);
        DestroyBlockOnFullyCleaned();
    }

    private void DecreaseScale(float decreasingValue) => transform.localScale -= new Vector3(decreasingValue, decreasingValue, decreasingValue);

    private void DestroyBlockOnFullyCleaned() {
        float valueToDestroy = 0.25f;
        if(_cleaningLeft <= valueToDestroy) {
            Destroy(gameObject);
        }
    }
}
