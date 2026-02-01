using UnityEngine;
using UnityEngine.UI;

public class WarpPos : MonoBehaviour
{
    public GameObject warpPos;
    public Button QTButton;

    public void Awake()
    {
        if (warpPos != null && QTButton != null)
        {

        }
        else if (warpPos == null)
            Debug.LogWarning("Warp Position is null");
        else if (QTButton != null)
            Debug.LogWarning("QT Button is null");

    }
}
