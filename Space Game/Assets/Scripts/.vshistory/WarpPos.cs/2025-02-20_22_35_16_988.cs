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

        CreateWarpButton();
    }

    private void CreateWarpButton()
    {

    }
}
