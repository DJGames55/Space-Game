using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class StarLocations : MonoBehaviour
{
    [System.Serializable]
    public class DistanceData
    {
        public string fromObject;
        public string toObject;
        public Vector3 distance;
    }

    public List<DistanceData> distances = new List<DistanceData>();

    public void CalculateDistances()
    {
        distances.Clear(); // Clear previous data

        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        for (int i = 0; i < children.Length; i++)
        {
            for (int j = 0; j < children.Length; j++)
            {
                if (i == j) continue; // Skip self-comparison

                Vector3 relativeDistance = children[j].position - children[i].position;
                distances.Add(new DistanceData
                {
                    fromObject = children[i].name,
                    toObject = children[j].name,
                    distance = relativeDistance
                });

                Debug.Log($"Distance from {children[i].name} to {children[j].name}: {relativeDistance}");
            }
        }
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