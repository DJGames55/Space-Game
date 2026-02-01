using System;
using UnityEngine;

[CreateAssetMenu(menuName = "StarLocationsData")]
public class StarLocationsData : ScriptableObject
{
    public List<StarLocations.DistanceData> savedDistances = new List<StarLocations.DistanceData>();

    public void Awake()
    {
        
    }
}
