using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject asteroidPrefab;  // Prefab to spawn
    [SerializeField] private Collider spawnArea;         // The area to populate (BoxCollider or SphereCollider)
    [SerializeField] private float minSpawnDistance = 4f; // Minimum distance between asteroids
    [SerializeField] private int maxAttempts = 1000;      // Safety limit to prevent infinite loops

    private List<Vector3> spawnedPositions = new List<Vector3>(); // Keep track of placed asteroids

    private void Start()
    {
        PopulateArea();
    }

    private void PopulateArea()
    {
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 spawnPoint = GetRandomPointInCollider();

            // Check if it's far enough from other asteroids
            bool validPosition = true;
            foreach (Vector3 existingPoint in spawnedPositions)
            {
                if (Vector3.Distance(spawnPoint, existingPoint) < minSpawnDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition)
            {
                Instantiate(asteroidPrefab, spawnPoint, Random.rotation);
                spawnedPositions.Add(spawnPoint); // Store the position
            }

            attempts++;
        }
    }

    private Vector3 GetRandomPointInCollider()
    {
        Bounds bounds = spawnArea.bounds;
        Vector3 randomPoint;

        if (spawnArea is BoxCollider boxCollider)
        {
            randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        else if (spawnArea is SphereCollider sphereCollider)
        {
            randomPoint = Random.insideUnitSphere * sphereCollider.radius + sphereCollider.transform.position;
        }
        else
        {
            Debug.LogWarning("Unsupported collider type. Please use a BoxCollider or SphereCollider.");
            return Vector3.zero;
        }

        return randomPoint;
    }
}
