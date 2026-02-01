using System.Collections;
using TMPro;
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

        _input.DebugEvent += ToggleDebugMenu;

        DebugMenu.SetActive(false);
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
        UpdateQTDistance();
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

    public void UpdateQTDistance()
    {
        foreach(Transform child in QTMenu.transform)
        {
            foreach(Transform child2 in child.transform)
            {
                float distanceToPos;

                if (child.GetComponent<QTButton>().overrideDistance)
                {
                    distanceToPos = Vector3.Distance(_ship.transform.position, child.GetComponent<QTButton>().distanceOverride);
                }
                else
                {
                    distanceToPos = Vector3.Distance(_ship.transform.position, child.GetComponent<QTButton>().warpPos.transform.position);
                }
                

                // Change Text to [Qt Name] - [Distance]
                child2.GetComponent<TextMeshProUGUI>().text = child.GetComponent<QTButton>().warpName + " - " + FormatDistance(distanceToPos); ;
            }
        }
    }

    // Converts distance to appropriate unit (m, km, Mm, Gm)
    private string FormatDistance(float distance)
    {
        if (distance >= 1_000_000_000_000_000f) // >1 Pm
            return $"{distance / 1_000_000_000_000_000:F2} Pm";
        else if (distance >= 1_000_000_000_000f) // >1 Tm
            return $"{distance / 1_000_000_000_000:F2} Tm";
        else if (distance >= 1_000_000_000f) // >= 1 Gm
            return $"{distance / 1_000_000_000f:F2} Gm";
        else if (distance >= 1_000_000f) // >= 1 Mm
            return $"{distance / 1_000_000f:F2} Mm";
        else if (distance >= 1_000f) // >= 1 km
            return $"{distance / 1_000f:F2} Km";
        else // Default: meters
            return $"{distance:F2} m";
    }

    #endregion

    // DebugMenu
    public GameObject DebugMenu;
    public void ToggleDebugMenu()
    {
        if (DebugMenu.gameObject.activeSelf)
        {
            DebugMenu.gameObject.SetActive(false);
        }
        else
        {
            DebugMenu.gameObject.SetActive(true);
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
