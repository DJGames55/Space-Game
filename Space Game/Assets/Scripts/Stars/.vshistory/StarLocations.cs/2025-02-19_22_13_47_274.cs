using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class StarLocations : MonoBehaviour
{
    [System.Serializable]
    public class DistanceData
    {
        public SceneField fromStar;
        public SceneField toStar;
        public Vector3 distance;
    }

    public List<DistanceData> distances = new List<DistanceData>();
    public StarLocationsData distanceStorage; // Assign this in Inspector

    public void CalculateDistances()
    {
        distances.Clear();
        if (distanceStorage != null)
            distanceStorage.savedDistances.Clear(); // Clear previous data

        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        for (int i = 0; i < children.Length; i++)
        {
            for (int j = 0; j < children.Length; j++)
            {
                if (i == j) continue;

                Vector3 relativeDistance = children[j].position - children[i].position;
                var data = new DistanceData
                {
                    fromStar = children[i].GetComponent<StarData>().starScene,
                    toStar = children[j].GetComponent<StarData>().starScene,
                    distance = relativeDistance
                };

                distances.Add(data);
                if (distanceStorage != null)
                    distanceStorage.savedDistances.Add(data);
            }
        }
        Debug.Log("Distances Calculated");
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

    public void Start()
    {
        LoadDistances();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(StarLocations))]
public class RunStarLoc : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws default inspector fields (so we can see the saved distances)

        var script = (StarLocations)target;
        if (GUILayout.Button("Recalculate Distance"))
        {
            script.CalculateDistances();
            EditorUtility.SetDirty(script); // Mark script as modified so changes persist
        }
    }
}
#endif