using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI FPSCounterText;
    private float fps;
    private int objectCount;
    private int renderedObjectCount;

    void Start()
    {
        InvokeRepeating("GetFPS", 0, 1);
    }

    
    private void GetFPS()
    {
        fps = (1f / Time.unscaledDeltaTime);
        FPSCounterText.text = "FPS - " + fps.ToString();
    }

    [System.Obsolete]
    private void CountObj()
    {
        // Count objects in the scene
        objectCount = FindObjectsOfType<GameObject>().Length;

        // Count rendered objects (using Renderer components)
        renderedObjectCount = 0;
        foreach (var renderer in FindObjectsOfType<Renderer>())
        {
            if (renderer.isVisible)
                renderedObjectCount++;
        }
    }
}
