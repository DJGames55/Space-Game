using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private ShipControls _ship;
    [SerializeField] private WeaponControls _weaponControls;
    [SerializeField] private StarLocationsData _starLocData;
    public static UIManager instance;

    public SceneField currentScene;

    [Header("Menus")]
    public string currentMenu;

    private void Awake()
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

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }

        if (currentScene == null)
            Debug.LogWarning("Current Scene not specified");
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

    [Header("QT Menus")]
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

        if ()

        StartCoroutine(Fade(QTMenuBackground, 0, 1, 0.5f));
    }

    public void CloseQT()
    {
        _input.SetShipControls();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(Fade(QTMenuBackground, 1, 0, 0.5f));

        QTMenuBackground.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void UpdateQTDistance()
    {
        foreach(Transform child in QTMenu.transform)
        {
            foreach (Transform child2 in child.transform)
            {
                if (child.GetComponent<QTButton>() != null && !child.GetComponent<QTButton>().currentlyDisabledInScene)
                {
                    float distanceToPos = 0f;
                    QTButton QTbutton = child.GetComponent<QTButton>();

                    if (QTbutton.warpPos == null && !QTbutton.starTravel)
                        QTbutton.GetWarpPos();

                    if (QTbutton.starTravel)
                        distanceToPos = Vector3.Distance(_ship.gameObject.transform.position, QTbutton.GetStarLocation(QTbutton.starScene));
                    else if (QTbutton.warpPos != null)
                        distanceToPos = Vector3.Distance(_ship.gameObject.transform.position, QTbutton.warpPos.transform.position);

                    // Change Text to [Qt Name] - [Distance]
                    child2.GetComponent<TextMeshProUGUI>().text = QTbutton.warpName + " - " + FormatDistance(distanceToPos);
                }
            }
        }
    }

    public void ReenableQTButtons()
    {
        foreach (Transform child in QTMenu.transform)
        {
            if (child.GetComponent<QTButton>() != null)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<QTButton>().SetSceneState();
            }
        }
    }

    // Converts distance to appropriate unit (m, km, Mm, Gm)
    private string FormatDistance(float distance)
    {
        if (distance >= 9_460_730_472_580_044f) // >1 Ly
            return $"{distance / 9_460_730_472_580_044:F2} ly";
        else if (distance >= 1_000_000_000_000_000f) // >1 Pm
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
