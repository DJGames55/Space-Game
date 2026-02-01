using UnityEngine;
using UnityEditor;
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


    public void CalculateDistances()
    {
        distances.Clear();
        DistanceManager.savedDistances.Clear(); // Store globally

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
                DistanceManager.savedDistances.Add(data); // Store globally
            }
        }
        Debug.Log("Distances Calculated");
    }
}

public static class DistanceManager
{
    public static List<StarLocations.DistanceData> savedDistances = new List<StarLocations.DistanceData>();
}

[CreateAssetMenu(fileName = "DistanceData", menuName = "Data/Distance Storage")]
public class DistanceDataSO : ScriptableObject
{
    public List<StarLocations.DistanceData> savedDistances = new List<StarLocations.DistanceData>();
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