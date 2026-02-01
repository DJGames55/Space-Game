using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] asteroidPrefabs; // Array of asteroid prefabs
    [SerializeField] private Collider spawnArea;          // The area where asteroids can spawn
    [SerializeField] private float minSpawnDistance = 4f; // Minimum distance between asteroids
    [SerializeField] private int maxAsteroids = 50;       // Max asteroids to spawn
    [SerializeField] private int maxAttempts = 1000;      // Prevents infinite loops

    [Header("Asteroid Size Settings")]
    [SerializeField] private float minScale = 0.5f;  // Smallest asteroid size
    [SerializeField] private float maxScale = 2.0f;  // Largest asteroid size

    private List<Vector3> spawnedPositions = new List<Vector3>(); // Tracks asteroid positions

    private void Start()
    {
        PopulateArea();
    }

    private void PopulateArea()
    {
        int spawnedCount = 0;
        int attempts = 0;

        while (spawnedCount < maxAsteroids && attempts < maxAttempts)
        {
            Vector3 spawnPoint = GetRandomPointInCollider();

            // Ensure asteroids don't spawn too close to each other
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
                // Pick a random asteroid prefab
                GameObject selectedPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

                // Spawn the asteroid as a child of this object
                GameObject newAsteroid = Instantiate(selectedPrefab, spawnPoint, Random.rotation, this.transform);

                // Set a random scale
                float randomScale = Random.Range(minScale, maxScale);
                newAsteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                spawnedPositions.Add(spawnPoint); // Store position
                spawnedCount++; // Increase spawned count
            }

            attempts++;
        }

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Asteroid spawning reached max attempts! Some asteroids may be missing.");
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
