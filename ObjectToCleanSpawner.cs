using UnityEngine;

public class ObjectToCleanSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startObjectToCleanPosition;

    [SerializeField] private GameObject[] _objectsToClean;

    [SerializeField] private Vector3[] _spawnOffset;

    public GameObject CurrentObjectAtCleaning{ get; private set; }

    public void SpawnRandomObjectForCleaning() {
        int targetObjectIndex = Random.Range(0, _objectsToClean.Length);
        Vector3 positionToSpawn = _startObjectToCleanPosition.position + _spawnOffset[targetObjectIndex];
        CurrentObjectAtCleaning = Instantiate(_objectsToClean[targetObjectIndex], positionToSpawn, Quaternion.identity);
    }
}
