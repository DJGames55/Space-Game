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

        SetWarpPos();
    }

    public void SetWarpPos()
    {
        if (_ui != null)
        {
            foreach (Transform child in _ui.QTMenu.transform)
            {
                if (child.tag == "LocalQTButton")
                {
                    child.GetComponent<QTButton>().warpPos = warpPos.transform.position;
                }
            }
        }
        else
            Debug.LogError("UIManager is null");
    }
}
