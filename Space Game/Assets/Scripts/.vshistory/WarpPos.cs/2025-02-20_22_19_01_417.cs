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

        SetWarpPos();
    }

    public void SetWarpPos()
    {
        Debug.Log("a");
        if (_ui != null)
        {
            foreach (Transform child in _ui.QTMenu.transform)
            {
                Debug.Log(child.name + "b");
                if (child.tag == "LocalQTButton" && child.GetComponent<QTButton>() != null)
                {
                    Debug.Log("c");
                    if (child.GetComponent<QTButton>().warpPosID == WarpPosID){
                        child.GetComponent<QTButton>().warpPos = warpPos.transform.position;
                        Debug.Log("d");
                    }
                }
            }
        }
        else
            Debug.LogError("UIManager is null");
    }
}
