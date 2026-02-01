using System.Collections.Generic;
using UnityEngine;

public class QTButton : MonoBehaviour
{
    [SerializeField] private WarpLoc _warpLoc;
    [SerializeField] private UIManager _ui;

    public string warpName;
    public string warpID;
    public GameObject warpPos;

    [Header("Star")]
    public bool starTravel;
    public SceneField starScene;
    [SerializeField] private StarLocationsData _starLocData;

    public bool currentlyDisabledInScene;

    public List<SceneField> enabledInScene = new List<SceneField>();

    private void Awake()
    {
        SetSceneState();
        GetWarpPos();
    }

    private void Start()
    {
        GetWarpPos();
    }

    public Vector3 GetStarLocation(SceneField targetStar)
    {
        foreach (StarLocations.DistanceData data in _starLocData.savedDistances)
        {
            if (data.fromStar.SceneName == _ui.currentScene.SceneName && data.toStar.SceneName == targetStar.SceneName)
                return (data.distance * 1_000_000_000_000_000);
        }

        return Vector3.zero;
    }

    public void SetSceneState()
    {
        if (!starTravel)
        {
            foreach (var item in enabledInScene) // Loop through all items in the list
            {
                if (item.SceneName == _ui.currentScene.SceneName)
                {
                    currentlyDisabledInScene = false;
                    break;
                }
                else
                    currentlyDisabledInScene = true;
            }
        }
        else
            if (starScene.SceneName == _ui.currentScene.SceneName)
                currentlyDisabledInScene = true;
        else
            currentlyDisabledInScene = false;

        if (currentlyDisabledInScene)
            gameObject.SetActive(false);
    }

    public void GetWarpPos()
    {
        foreach (PosData posData in _warpLoc.warpLocations)
        {
            if (posData.WarpID == warpID)
            {
                posData.WarpLoc.gameObject.SetActive(false);
                warpPos = posData.WarpLoc;

                if (posData.WarpPosition != null)
                    warpPos = posData.WarpPosition.gameObject;

                return;
            }
        }
    }
}
