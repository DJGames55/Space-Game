using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    private float fps;
    public TMPro.TextMeshProUGUI FPSCounterText;

    void Start()
    {
        InvokeRepeating("GetFPS", 0, 1);
    }

    
    private void GetFPS()
    {
        fps = (1f / Time.unscaledDeltaTime);
        FPSCounterText.text = "FPS - " + fps.ToString();
    }
}
