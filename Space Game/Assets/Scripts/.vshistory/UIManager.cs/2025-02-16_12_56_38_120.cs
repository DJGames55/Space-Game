using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private ShipControls _ship;

    public string currentMenu;


    private void Start()
    {
        Menu.SetActive(true);
        Menu.GetComponent<CanvasGroup>().alpha = 0;
        QTMenu.SetActive(true);
        QTMenuBackground.SetActive(true);
        QTMenuBackground.GetComponent<CanvasGroup>().alpha = 0;

        _input.OpenQTEvent += OpenQTMenu;

        Menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        QTMenuBackground.GetComponent<CanvasGroup>().blocksRaycasts = false;


        DebugMenu.SetActive(true);
        DebugMenu.GetComponent<CanvasGroup>().alpha = 0;
        DebugMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        DebugMenu.GetComponent<CanvasGroup>().interactable = false;
    }

    // Menu
    public GameObject Menu;
    #region Menu

    public void Pause()
    {
        Menu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        currentMenu = "pauseMenu";
        StartCoroutine(Fade(Menu, 0, 1, 0.2f));
    }

    public void Resume()
    {
        switch (currentMenu)
        {
            case "pauseMenu":
                Menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
                StartCoroutine(Fade(Menu, 1, 0, 0.2f));
                break;
            case "QTMenu":
                QTMenuBackground.GetComponent<CanvasGroup>().blocksRaycasts = false; 
                StartCoroutine(Fade(QTMenuBackground, 1, 0, 0.2f));
                break;
        }

        currentMenu = null;
    }

    #endregion

    // QT Menu
    public GameObject QTMenu;
    public GameObject QTMenuBackground;
    #region QT Menu

    public void OpenQTMenu()
    {
        QTMenuBackground.GetComponent<CanvasGroup>().blocksRaycasts = true;
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

        QTMenuBackground.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    #endregion

    // DebugMenu
    public GameObject DebugMenu;
    public void ToggleDebugMenu()
    {
        if (DebugMenu.GetComponent<CanvasGroup>().alpha == 0)
        {
            DebugMenu.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            DebugMenu.GetComponent<CanvasGroup>().alpha = 0;
        }
    }


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
