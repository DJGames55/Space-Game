using UnityEngine;

public class QTButton : MonoBehaviour
{
    [SerializeField] private UIManager _ui;

    public string warpName;
    public GameObject warpPos;

    [Header("Star")]
    public bool starTravel;
    public SceneField starScene;
    public Vector3 starLocation;
    [SerializeField] private StarLocationsData _starLocData;

    [Header("Misc")]
    public bool disabledInScene;

    private void Awake()
    {
        if (disabledInScene)
        {
            gameObject.SetActive(false);
        }
        else if (warpPos == null && !starTravel)
        {
            Debug.LogError($"{gameObject.name} Does not have a Warp Position specified. You may need to set starTravel to True/Disable Button");
        }
        else if (starTravel)
            starLocation = GetStarLocation(starScene);
    }

    private Vector3 GetStarLocation(SceneField targetStar)
    {
        foreach (StarLocations.DistanceData data in _starLocData.savedDistances)
        {
            if (data.fromStar.SceneName == _ui.currentScene.SceneName && data.toStar.SceneName == targetStar.SceneName)
                return (data.distance * 1_000_000_000_000_000);
        }

        return Vector3.zero;
    }
}
