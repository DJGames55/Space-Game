using UnityEngine;

public class WarpPos : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private GameObject buttonTemp;

    [System.Serializable]
    public class warpInfo
    {
        public string WarpID;
        public GameObject warpPos;
        public SceneField activeScene;
    }

    public void Awake()
    {
        if (_ui == null)
            Debug.LogError("UIManager is null");
        if (buttonTemp == null)
            Debug.LogError("Button Template is null");
    }
}
