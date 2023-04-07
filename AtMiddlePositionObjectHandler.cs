using UnityEngine;

public class AtMiddlePositionObjectHandler : MonoBehaviour
{
    public static bool IsTargetObjectAtMiddlePosition{ get; set; }

    private const string _middlePositionObjectTag = "MiddlePositionOfObjectToClean";

    private void Awake() => IsTargetObjectAtMiddlePosition = false;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == _middlePositionObjectTag) IsTargetObjectAtMiddlePosition = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == _middlePositionObjectTag) IsTargetObjectAtMiddlePosition = false;
    }
}
