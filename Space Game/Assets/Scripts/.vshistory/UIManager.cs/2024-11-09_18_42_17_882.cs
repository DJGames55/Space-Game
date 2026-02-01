using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    private void Start()
    {
        Menu.SetActive(true);
        Menu.GetComponent<CanvasGroup>().alpha = 0;
        QTMenu.SetActive(true);
        QTMenuBackground.GetComponent<CanvasGroup>().alpha = 0;

        _input.OpenQTEvent += openQT;
    }

    // Menu
    public GameObject Menu;
    #region Menu

    public void Pause()
    {
        StartCoroutine(Fade(Menu, 0, 1, 0.2f));
    }

    public void Resume()
    {
        StartCoroutine(Fade(Menu, 1, 0, 0.2f));
    }

    #endregion

    // QT Menu
    public GameObject QTMenu;
    public GameObject QTMenuBackground;
    #region QT Menu

    public void openQT()
    {
        Debug.Log("Open QT");
        StartCoroutine(openingQT());
    }

    public IEnumerator openingQT()
    {
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(Fade(QTMenuBackground, 0, 1, 0.02f));
        yield return new WaitForSeconds(0.3f);
    }
    #endregion

    public IEnumerator Fade(GameObject uiObject, float start, float end, float duration)
    {
        float counter = 0f;
        CanvasGroup canvasGroup = uiObject.GetComponent<CanvasGroup>();

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter/duration);

            yield return null;
        }
    }
}
