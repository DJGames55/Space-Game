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

        _input.OpenQTEvent += OpenQTMenu;
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

    public void OpenQTMenu()
    {
        _input.SetUI();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        QTMenu.GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(OpenQT());
    }

    public IEnumerator OpenQT()
    {
        StartCoroutine(Fade(QTMenuBackground, 0, 1, 0.5f));
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Fade(QTMenu, 0, 1, 0.1f));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Fade(QTMenu, 0, 1, 0.1f));

    }

    public void CloseQT()
    {
        _input.SetShipControls();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
