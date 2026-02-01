using System.Collections;
using UnityEngine;

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

        StartCoroutine(SetWarpPos());
    }

    public IEnumerator SetWarpPos()
    {
        yield return new WaitForFixedUpdate();

        if (_ui != null)
        {
            foreach (Transform child in _ui.QTMenu.transform)
            {
                if (child.tag == "LocalQTButton" && child.GetComponent<QTButton>() != null)
                {
                    if (child.GetComponent<QTButton>().warpPosID == WarpPosID)
                    {
                        child.GetComponent<QTButton>().warpPos = warpPos;
                        Debug.Log($"{child} d {warpPos} - {child.GetComponent<QTButton>().warpPos}");
                    }
                }
            }
        }
        else
            Debug.LogError("UIManager is null");
    }
}
