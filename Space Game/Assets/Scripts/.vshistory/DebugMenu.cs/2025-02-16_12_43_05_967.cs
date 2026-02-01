using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI FPSCounterText;
    [SerializeField] private TMPro.TextMeshProUGUI ObjCountText;
    [SerializeField] private TMPro.TextMeshProUGUI ObjRenderedText;
    private float fps;
    private int objectCount;
    private int renderedObjectCount;

    private void Update()
    {
        GetFPS();
        CountObj();
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
        objectCount = Object.FindObjectsOfType<GameObject>().Length;

        // Count rendered objects (using Renderer components)
        renderedObjectCount = 0;
        foreach (var renderer in Object.FindObjectsOfType<Renderer>())
        {
            if (renderer.isVisible)
                renderedObjectCount++;
        }

        ObjCountText.text = "Obj Count - " + objectCount.ToString();
        ObjRenderedText.text = "Obj Rendered - " + renderedObjectCount.ToString();
    }
}
