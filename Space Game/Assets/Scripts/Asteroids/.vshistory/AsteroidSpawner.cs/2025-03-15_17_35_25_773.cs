using UnityEngine;
using System.Collections.Generic;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

[ExecuteInEditMode]
public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Asteriods[] asteroidPrefabs; // Array of asteroid prefabs
    [SerializeField] private Collider spawnArea;          // BoxCollider or SphereCollider defining the spawn area
    [SerializeField] private float minSpawnDistance = 4f; // Minimum distance between asteroids
    [SerializeField] private int maxAttempts = 5000;      // Safety limit to prevent infinite loops

    [Header("Asteroid Size Settings")]
    [SerializeField] private float minScale = 0.5f;  // Smallest possible asteroid size
    [SerializeField] private float maxScale = 2.0f;  // Largest possible asteroid size

    private List<Vector3> spawnedPositions = new List<Vector3>(); // Stores spawned positions

    private void Start()
    {
        if (maxAttempts > 7500)
        {
            Debug.LogWarning("Exceeding Recomended count, proceed with Caution"); 
            Debug.LogError("Editor Crash Likely");
        }

        PopulateArea();
    }

    public void PopulateArea()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

#if UNITY_EDITOR
        UnityEditor.SceneView.RepaintAll();
#endif

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
                // Generate a random number from 1 to 10 (inclusive)
                int spawnNum = UnityEngine.Random.Range(1, 11);

                // Get a list of valid asteroids based on spawnPriority (excluding priority 0)
                List<Asteriods> validAsteroids = new List<Asteriods>();

                foreach (Asteriods asteroid in asteroidPrefabs)
                {
                    if (asteroid.SpawnPriority >= spawnNum && asteroid.SpawnPriority > 0)
                    {
                        validAsteroids.Add(asteroid);
                    }
                }

                // If no valid asteroids are found, fallback to a random asteroid (excluding 0-priority ones)
                Asteriods chosenAsteroid;
                if (validAsteroids.Count > 0)
                {
                    chosenAsteroid = validAsteroids[UnityEngine.Random.Range(0, validAsteroids.Count)];
                }
                else
                {
                    // Get a random asteroid but ensure its priority is > 0
                    List<Asteriods> nonZeroAsteroids = new List<Asteriods>();
                    foreach (Asteriods asteroid in asteroidPrefabs)
                    {
                        if (asteroid.SpawnPriority > 0)
                        {
                            nonZeroAsteroids.Add(asteroid);
                        }
                    }

                    // Ensure there's at least one valid asteroid
                    if (nonZeroAsteroids.Count > 0)
                    {
                        chosenAsteroid = nonZeroAsteroids[UnityEngine.Random.Range(0, nonZeroAsteroids.Count)];
                    }
                    else
                    {
                        Debug.LogWarning("No valid asteroids available!");
                        return;
                    }
                }
            }
        }
    }

    private Vector3 GetRandomPointInCollider()
    {
        Bounds bounds = spawnArea.bounds;
        Vector3 randomPoint;

        if (spawnArea is BoxCollider boxCollider)
        {
            randomPoint = new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
            );
        }
        else if (spawnArea is SphereCollider sphereCollider)
        {
            randomPoint = UnityEngine.Random.insideUnitSphere * sphereCollider.radius + sphereCollider.transform.position;
        }
        else
        {
            Debug.LogWarning("Unsupported collider type. Please use a BoxCollider or SphereCollider.");
            return Vector3.zero;
        }

        return randomPoint;
    }
}

[Serializable]
public class Asteriods
{
    public GameObject Asteroid;
    [Range(0f, 10f)] public int SpawnPriority;
}


[CustomEditor(typeof(AsteroidSpawner))]
public class AsteroidSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws default Inspector UI

        AsteroidSpawner spawner = (AsteroidSpawner)target;
        if (GUILayout.Button("Populate Asteroids"))
            spawner.PopulateArea();
    }
}