using UnityEngine;

public class FrameRendererHandler : MonoBehaviour
{
    private void Awake() => OptimizeFrameRenderPerformance();

    public void OptimizeFrameRenderPerformance() {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}
