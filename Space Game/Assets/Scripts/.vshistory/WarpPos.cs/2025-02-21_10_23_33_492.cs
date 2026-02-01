using UnityEngine;

public class WarpPos : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private GameObject buttonTemp;

    [System.Serializable]
    public class warp
    {
        public string WarpID;
        public GameObject warpPos;
        public SceneField activeScene;
    }

    public warp warpInfo;

    public void Awake()
    {
        if (warpInfo.WarpID == null)
            Debug.LogError("Warp ID is null");
        if (_ui == null)
            Debug.LogError("UIManager is null");
        if (buttonTemp == null)
            Debug.LogError("Button Template is null");
    }
}
