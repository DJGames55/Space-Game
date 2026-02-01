using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private GameObject[] asteroidPrefabs;  // Array of asteroid prefabs
    [SerializeField] private int asteroidCount = 100;       // Number of asteroids to spawn
    [SerializeField] private Vector2 scaleRange = new Vector2(0.5f, 3f); // Min and max scale
    [SerializeField] private float fieldRadius;       // Radius of the spawn field
    [SerializeField] private float minSpawnDistance = 4f;   // Minimum distance from the center
    [SerializeField] private float centerBias = 2f;         // Higher values make more spawn near the center

    private void Start()
    {
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

        // Get a random point biased towards the center but outside the min spawn distance
        Vector3 randomPosition = GetBiasedPosition();

        // Pick a random asteroid prefab
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Instantiate asteroid
        GameObject asteroid = Instantiate(asteroidPrefab, randomPosition, Random.rotation, transform);

        // Adjust scale: larger near center, smaller near edges
        float distanceFromCenter = (randomPosition - transform.position).magnitude / fieldRadius;
        float scaleMultiplier = Mathf.Lerp(scaleRange.y, scaleRange.x, distanceFromCenter);
        asteroid.transform.localScale = Vector3.one * scaleMultiplier;
    }

    private Vector3 GetBiasedPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized; // Random direction

        // Get a random distance biased toward the center but respecting the minSpawnDistance
        float randomDistance = Mathf.Pow(Random.value, centerBias) * (fieldRadius - minSpawnDistance) + minSpawnDistance;

        return transform.position + randomDirection * randomDistance;
    }
}
