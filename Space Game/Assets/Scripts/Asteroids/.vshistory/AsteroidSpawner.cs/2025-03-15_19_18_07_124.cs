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

        Destroy(spawnArea);
    }

    public void ClearArea()
    {
        // Clear existing objects safely
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            DestroyImmediate(transform.GetChild(i).gameObject); // Works only in editor
#else
            Destroy(transform.GetChild(i).gameObject); // Use normal destroy in play mode
#endif
        }

#if UNITY_EDITOR
        UnityEditor.SceneView.RepaintAll(); // Refresh scene view in editor
#endif
    }


    public void PopulateArea()
    {
        if (maxAttempts > 7500)
        {
            Debug.LogWarning("Exceeding Recomended count, proceed with Caution");
            Debug.LogError("Editor Crash Likely");
        }

        ClearArea();

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
                // Generate a random number from 1 to 10
                int spawnNum = UnityEngine.Random.Range(1, 11); // Inclusive of 10

                // Get a list of valid asteroids based on spawnPriority
                List<Asteriods> validAsteroids = new List<Asteriods>();
                foreach (Asteriods asteroid in asteroidPrefabs)
                {
                    if (asteroid.SpawnPriority >= spawnNum)
                    {
                        validAsteroids.Add(asteroid);
                    }
                }

                // If no valid asteroids found, pick a random one (fallback)
                Asteriods chosenAsteroid;
                if (validAsteroids.Count > 0)
                {
                    chosenAsteroid = validAsteroids[UnityEngine.Random.Range(0, validAsteroids.Count)];
                }
                else
                {
                    chosenAsteroid = asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)];
                }

                GameObject selectedPrefab = chosenAsteroid.Asteroid;

                // Spawn the asteroid and make it a child of this object
                GameObject newAsteroid = Instantiate(selectedPrefab, spawnPoint, UnityEngine.Random.rotation, this.transform);

                // Set a random scale
                float randomScale = UnityEngine.Random.Range(minScale, maxScale);
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
        if (GUILayout.Button("Clear Area"))
            spawner.ClearArea();

        if (GUILayout.Button("Populate Asteroids"))
            spawner.PopulateArea();
    }
}