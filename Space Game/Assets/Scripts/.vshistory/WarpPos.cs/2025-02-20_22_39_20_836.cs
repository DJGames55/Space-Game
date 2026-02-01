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
            Debug.LogError("Buttom Template is null");

        CreateWarpButton();
    }

    private void CreateWarpButton()
    {
        GameObject newButton = Instantiate(buttonTemp, _ui.QTMenu.transform);
        QTButton qtButton = newButton.GetComponent<QTButton>();

        if (qtButton != null )
        {
            qtButton.warpPos = warpPos;
        }

        foreach (Transform child in transform)
        {
            child.GetComponent<TextMeshProUGUI>().text = warpName;
        }
    }
}
