using System.Collections.Generic;
using UnityEngine;

public class StarDistances : MonoBehaviour
{
    private Dictionary<GameObject, List<ObjectData>> objectDataMap = new Dictionary<GameObject, List<ObjectData>>();
    private List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        GetChildrenObjects();
        if (objects.Count < 2)
        {
            Debug.LogWarning("Not enough child objects to calculate distances and angles.");
            return;
        }
        CalculateDistancesAndAngles();
    }

    void GetChildrenObjects()
    {
        objects.Clear();
        foreach (Transform child in transform) // Loop through all children
        {
            objects.Add(child.gameObject);
        }
    }

    void CalculateDistancesAndAngles()
    {
        objectDataMap.Clear();

        foreach (GameObject objA in objects)
        {
            List<ObjectData> dataList = new List<ObjectData>();

            foreach (GameObject objB in objects)
            {
                if (objA == objB) continue; // Skip self-comparison

                float distance = Vector3.Distance(objA.transform.position, objB.transform.position);
                Vector3 direction = (objB.transform.position - objA.transform.position).normalized;
                float angle = Vector3.Angle(objA.transform.forward, direction);

                dataList.Add(new ObjectData(objB, distance, angle));
            }

            objectDataMap[objA] = dataList;
        }

        PrintData();
    }

    void PrintData()
    {
        foreach (var entry in objectDataMap)
        {
            Debug.Log($"Object: {entry.Key.name}");
            foreach (var data in entry.Value)
            {
                Debug.Log($"   → {data.target.name}: Distance = {data.distance:F2}, Angle = {data.angle:F2}");
            }
        }
    }
}

[System.Serializable]
public class ObjectData
{
    public GameObject target;
    public float distance;
    public float angle;

    public ObjectData(GameObject target, float distance, float angle)
    {
        this.target = target;
        this.distance = distance;
        this.angle = angle;
    }
}
