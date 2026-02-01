using UnityEngine;
using UnityEngine.UI;

public class WarpPos : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    public GameObject warpPos;
    public string WarpPosID;

    public void Awake()
    {
        if (warpPos == null)
            Debug.LogWarning("Warp Position is null");
        if (WarpPosID == null)
            Debug.LogError("Warp Position ID is null");
        if (_ui == null)
            Debug.LogError("UIManager is null");
    }

    public void SetWarpPos()
    {
        foreach (Transform child in _ui.QTMenu.transform)
        {
            if (child.tag == "LocalQTButton")
            {

            }
        }
    }
}
