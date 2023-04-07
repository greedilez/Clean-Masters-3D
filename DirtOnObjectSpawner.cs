using UnityEngine;

public class DirtOnObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _allDirtAtObject;

    private int _dirtSpawned = 0;

    private void Awake() => EnableDirtInRandomOrder();

    private void EnableDirtInRandomOrder() {
        int iterationsCount = 0;
        for (int i = 0; i < _allDirtAtObject.Length; i++) {
            _allDirtAtObject[i].SetActive(WillDirtBeEnabled());
            iterationsCount++;
        }

        if (iterationsCount >= _allDirtAtObject.Length && (_dirtSpawned == 0)) {
            _allDirtAtObject[Random.Range(0, _allDirtAtObject.Length)].SetActive(true);
        }
    }

    private bool WillDirtBeEnabled() {
        int minimalChance = 1;
        int maximalChance = 10;
        int resultChance = Random.Range(minimalChance, maximalChance);
        int resultChanceToSpawn = 5;
        if (resultChance > resultChanceToSpawn) {
            _dirtSpawned++;
            return true;
        }
        return false;
    }
}
