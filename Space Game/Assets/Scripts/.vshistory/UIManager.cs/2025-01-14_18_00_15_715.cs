using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    public string currentMenu;

    private void Start()
    {
        Menu.SetActive(true);
        Menu.GetComponent<CanvasGroup>().alpha = 0;
        QTMenu.SetActive(true);
        QTMenuBackground.SetActive(true);
        QTMenuBackground.GetComponent<CanvasGroup>().alpha = 0;

        _input.OpenQTEvent += OpenQTMenu;

        Menu.SetActive(false);
        QTMenuBackground.SetActive(false);
    }

    // Menu
    public GameObject Menu;
    #region Menu

    public void Pause()
    {
        Menu.SetActive(true);
        currentMenu = "pauseMenu";
        StartCoroutine(Fade(Menu, 0, 1, 0.2f));
    }

    public void Resume()
    {
        Debug.Log("Resume");
        switch (currentMenu)
        {
            case "pauseMenu":
                Debug.Log("Pause");
                break;
            case "QTMenu":
                Debug.Log("QT");
                break;
        }

        currentMenu = "null";
        StartCoroutine(Fade(Menu, 1, 0, 0.2f));
    }

    #endregion

    // QT Menu
    public GameObject QTMenu;
    public GameObject QTMenuBackground;
    #region QT Menu

    public void OpenQTMenu()
    {
        QTMenu.SetActive(true);
        currentMenu = "QTMenu";
        _input.SetUI();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(Fade(QTMenuBackground, 0, 1, 0.5f));
        StartCoroutine(Fade(QTMenu, 0, 1, 0.5f));
    }

    public void CloseQT()
    {
        _input.SetShipControls();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(Fade(QTMenuBackground, 1, 0, 0.5f));
        StartCoroutine(Fade(QTMenu, 1, 0, 0.5f));

        QTMenu.SetActive(false);
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
