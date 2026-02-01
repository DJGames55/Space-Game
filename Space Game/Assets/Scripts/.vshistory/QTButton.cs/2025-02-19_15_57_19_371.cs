using UnityEngine;

public class QTButton : MonoBehaviour
{
    

    public string warpName;
    public GameObject warpPos;
    public bool overrideDistance;
    public Vector3 distanceOverride;

    [Header("Star")]
    public bool starTravel;
    public SceneField starScene;

    [Header("Misc")]
    public bool diabledInScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (diabledInScene)
        {
            Destroy(gameObject);
        }

        if (warpPos = null)
        {
            Debug.LogError($"{gameObject.name} Does not have a Warp Position specified. You may need to set overrideDistance to True/Disable Button");
        }
    }
}
