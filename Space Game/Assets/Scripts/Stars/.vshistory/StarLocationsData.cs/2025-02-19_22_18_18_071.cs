using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarLocationsData", menuName = "Data/Distance Storage")]
public class StarLocationsData : ScriptableObject
{
    public List<StarLocations.DistanceData> savedDistances = new List<StarLocations.DistanceData>();

    public void Awake()
    {
        
    }
}
