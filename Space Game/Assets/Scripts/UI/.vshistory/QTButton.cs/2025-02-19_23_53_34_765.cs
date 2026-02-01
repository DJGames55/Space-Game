using UnityEngine;

public class QTButton : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    [SerializeField] private StarLocationsData _starLocData;

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
        else if (warpPos == null && !starTravel)
        {
            Debug.LogError($"{gameObject.name} Does not have a Warp Position specified. You may need to set starTravel to True/Disable Button");
        }
    }

    private Vector3 GetStarLocation()
    {
        foreach (StarLocations.DistanceData data in _starLocData.savedDistances)
        {
            if (data.fromStar.SceneName == _ui.currentScene.SceneName && data.toStar.SceneName == targetStar.SceneName)
            {
                
                return Vector3.zero;
            }
        }

        return Vector3.zero;
    }
}
