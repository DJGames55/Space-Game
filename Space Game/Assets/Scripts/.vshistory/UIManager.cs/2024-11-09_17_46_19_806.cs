using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private void Start()
    {
        Menu.SetActive(true);
        Menu.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Menu
    public GameObject Menu;
    #region Menu

    public void Pause()
    {
        StartCoroutine(Fade(Menu.GetComponent<CanvasGroup>(), 0, 1, 0.2f));
    }

    public void Resume()
    {
        StartCoroutine(Fade(Menu.GetComponent<CanvasGroup>(), 1, 0, 0.2f));
    }

    #endregion

    // QT Menu
    public GameObject QTMenu;
    #region QT Menu
    public void openQT()
    {
        StartCoroutine(Fade());
    }
    #endregion

    public IEnumerator Fade(GameObject uiObject, float start, float end, float duration)
    {
        float counter = 0f;
        CanvasGroup canvasGroup = uiObject.GetComponent<CanvasGroup>();

        if (canvasGroup != null )
        {
            yield return null;
        }

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter/duration);

            yield return null;
        }
    }
}
