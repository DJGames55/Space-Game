using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private GameObject[] asteroidPrefabs; // Array of asteroid prefabs
    [SerializeField] private int asteroidCount = 10; // Number of asteroids to spawn
    [SerializeField] private Vector2 scaleRange = new Vector2(0.5f, 3f); // Min and max scale

    private Collider spawnArea; // Reference to the collider defining the spawn area

    private void Start()
    {
        spawnArea = GetComponent<Collider>();

        SpawnAsteroids();
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefabs.Length == 0) return;

        // Pick a random asteroid prefab
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Generate a random position inside the collider bounds
        Vector3 randomPosition = GetRandomPositionWithinBounds();

        // Instantiate asteroid
        GameObject asteroid = Instantiate(asteroidPrefab, randomPosition, Random.rotation, transform);

        // Set random scale
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        asteroid.transform.localScale = Vector3.one * randomScale;
    }

    private Vector3 GetRandomPositionWithinBounds()
    {
        Bounds bounds = spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
