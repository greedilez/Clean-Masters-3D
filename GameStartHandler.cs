using UnityEngine.Events;
using UnityEngine;

public class GameStartHandler : MonoBehaviour
{
    public UnityEvent OnGameStarted;

    public bool IsGameStarted{ get; private set; }

    private void Update() => TrackForFirstTap();

    private void TrackForFirstTap() {
        if (!IsGameStarted) {
            if(Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began) {
                    OnGameStarted.Invoke();
                    IsGameStarted = true;
                    Debug.Log("Game has been started!");
                }
            }
        }
    }
}
