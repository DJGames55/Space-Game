using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StarLocationsData")]
public class StarLocationsData : ScriptableObject
{
    public List<StarLocations.DistanceData> savedDistances = new List<StarLocations.DistanceData>();

    public void Awake()
    {
        Debug.Log("A");
    }

    public void SaveDistances()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/distances.json", json);
        Debug.Log("Distances saved to " + Application.persistentDataPath);
    }

    public void LoadDistances()
    {
        string path = Application.persistentDataPath + "/distances.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
            Debug.Log("Distances loaded from " + path);
        }
    }
}
