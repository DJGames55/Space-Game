using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    private void Start()
    {
        Menu.SetActive(true);
        Menu.GetComponent<CanvasGroup>().alpha = 0;
        QTMenu.SetActive(true);
        QTMenuBackground.SetActive(true);
        QTMenuBackground.GetComponent<CanvasGroup>().alpha = 0;

        _input.OpenQTEvent += OpenQTMenu;

        CreateQTMenu();
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

    [SerializeField] private GameObject buttonPrefab; // Assign a Button prefab in the inspector
    [SerializeField] private Transform buttonParent; // Assign the parent transform (e.g., QTMenu) in the inspector
    [SerializeField] private List<string> buttonLabels; // Specify button labels in the inspector

    public void CreateQTMenu()
    {
        // Clear any existing buttons
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // Create buttons dynamically
        foreach (string label in buttonLabels)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            Text buttonText = newButton.GetComponentInChildren<Text>();

            if (buttonText != null)
            {
                buttonText.text = label;
            }

            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                // Optionally add listeners or functionality here
                buttonComponent.onClick.AddListener(() => Debug.Log($"{label} button clicked!"));
            }
        }
    }


    public void OpenQTMenu()
    {
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
