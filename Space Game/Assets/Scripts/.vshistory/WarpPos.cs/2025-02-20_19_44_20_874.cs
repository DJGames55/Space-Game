using UnityEngine;
using UnityEngine.UI;

public class WarpPos : MonoBehaviour
{
    public GameObject warpPos;
    public string WarpPosID;

    public void Awake()
    {
        if (warpPos == null)
            Debug.LogWarning("Warp Position is null");
        if (WarpPosID == null)
            Debug.LogError("Warp Position ID is null");
    }
}
