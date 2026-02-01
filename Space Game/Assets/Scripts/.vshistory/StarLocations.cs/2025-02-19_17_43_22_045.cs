using UnityEngine;

public class StarLocations : MonoBehaviour
{
    public void CalculateDistances()
    {
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
                Debug.Log($"Distance from {children[i].name} to {children[j].name}: {relativeDistance}");
            }
        }
    }
}
