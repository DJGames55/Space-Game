using UnityEngine;

public class QTButton : MonoBehaviour
{
    public string warpName;
    public GameObject warpPos;

    [Header("Star")]
    public bool starTravel;
    public SceneField starScene;

    [Header("Misc")]
    public bool disabledInScene;

    private void Awake()
    {
        if (disabledInScene)
        {
            Destroy(gameObject);
        }

        if (warpPos == null && !starTravel && disabledInScene)
        {
            Debug.LogError($"{gameObject.name} Does not have a Warp Position specified. You may need to set starTravel to True/Disable Button");
        }
    }
}
