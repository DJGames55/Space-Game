using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class QTButton : MonoBehaviour
{
    [SerializeField] private UIManager _ui;

    public string warpName;
    public Vector3 warpPos;
    public string warpPosID;

    [Header("Star")]
    public bool starTravel;
    public SceneField starScene;
    public Vector3 starLocation;
    [SerializeField] private StarLocationsData _starLocData;

    public bool currentlyDisabledInScene;

    public List<SceneField> disabledInScene = new List<SceneField>();

    private void Awake()
    {
        SetSceneState();
        GetWarpPos();

        if (warpPos == null && !starTravel)
        {
            Debug.LogError($"{gameObject.name} Does not have a Warp Position specified. You may need to set starTravel to True/Disable Button or Disable in Current Scene");
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

    public void SetSceneState()
    {
        foreach (var item in disabledInScene) // Loop through all items in the list
        {
            if (item.SceneName == _ui.currentScene.SceneName)
            {
                currentlyDisabledInScene = true;
                break;
            }
            else
                currentlyDisabledInScene = false;
        }

        if (currentlyDisabledInScene)
            gameObject.SetActive(false);
    }

    private void GetWarpPos()
    {
        foreach (var warp in Object.FindObjectsByType<WarpPos>(FindObjectsSortMode.None))
        {
            if (warp.GetComponent<WarpPos>().WarpPosID == warpPosID)
                warpPos = warp.GetComponent<WarpPos>().warpPos;
        }
    }

}
