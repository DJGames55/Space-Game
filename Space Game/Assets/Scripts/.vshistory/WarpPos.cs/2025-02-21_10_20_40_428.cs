using TMPro;
using UnityEngine;

public class WarpPos : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private GameObject buttonTemp;
    public string warpID;
    public GameObject warpPos;

    public void Awake()
    {
        if (warpPos == null)
            Debug.LogWarning("Warp Position is null");
        if (_ui == null)
            Debug.LogError("UIManager is null");
        if (buttonTemp == null)
            Debug.LogError("Button Template is null");
    }
}
