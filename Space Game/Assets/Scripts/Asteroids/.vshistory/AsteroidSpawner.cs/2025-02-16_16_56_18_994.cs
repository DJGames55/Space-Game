using UnityEngine;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] asteroidPrefabs; // Array of asteroid prefabs
    [SerializeField] private Collider spawnArea;          // BoxCollider or SphereCollider defining the spawn area
    [SerializeField] private float minSpawnDistance = 4f; // Minimum distance between asteroids
    [SerializeField] private int maxAttempts = 5000;      // Safety limit to prevent infinite loops
    [SerializeField] private bool ignoreReccomend = false;

    [Header("Asteroid Size Settings")]
    [SerializeField] private float minScale = 0.5f;  // Smallest possible asteroid size
    [SerializeField] private float maxScale = 2.0f;  // Largest possible asteroid size

    private List<Vector3> spawnedPositions = new List<Vector3>(); // Stores spawned positions

    private void Start()
    {
        if (maxAttempts > 7500)
        {
            Debug.LogWarning("Exceeding Recomended count, proceed with Caution");
        }
        else if (ignoreReccomend)
        {
            Debug.LogError("Editor Crash Likely");
            PopulateArea();
        }
    }

    private void PopulateArea()
    {
        int attempts = 0;

        while (attempts < maxAttempts)
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
                // Pick a random asteroid prefab from the array
                GameObject selectedPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

                // Spawn the asteroid and make it a child of this object
                GameObject newAsteroid = Instantiate(selectedPrefab, spawnPoint, Random.rotation, this.transform);

                // Set a random scale
                float randomScale = Random.Range(minScale, maxScale);
                newAsteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

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
