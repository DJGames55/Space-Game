using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Menu
    public GameObject Menu;
    #region Menu

    private CanvasGroup MenuCanvas;

    public void Pause()
    { 
        MenuCanvas = Menu.GetComponent<CanvasGroup>();
        Menu.SetActive(true);
    }

    public void Resume()
    {
        Menu.SetActive(false);
    }

    #endregion
    public IEnumerator Fade(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter/duration);
        }

        return null;
    }
}
