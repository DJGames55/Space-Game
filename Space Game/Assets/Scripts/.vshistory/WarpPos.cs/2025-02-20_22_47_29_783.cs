using TMPro;
using UnityEngine;

public class WarpPos : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private GameObject buttonTemp;
    public string warpName;
    public GameObject warpPos;

    public void Awake()
    {
        if (warpPos == null)
            Debug.LogWarning("Warp Position is null");
        if (_ui == null)
            Debug.LogError("UIManager is null");
        if (buttonTemp == null)
            Debug.LogError("Button Template is null");

        CreateWarpButton();
    }

    private void CreateWarpButton()
    {
        GameObject newButton = Instantiate(buttonTemp);
        QTButton qtButton = newButton.GetComponent<QTButton>();
        newButton.name = warpName + "QTButton";
        newButton.transform.SetParent(_ui.QTMenu.transform, false);
        newButton.transform.SetSiblingIndex(0);

        if (qtButton != null)
        {
            qtButton.warpPos = warpPos;
            qtButton.name = warpName;
            qtButton.enabledInScene.Add(_ui.currentScene);
        }
    }
}
