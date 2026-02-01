using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private GameObject[] asteroidPrefabs;  // Array of asteroid prefabs
    [SerializeField] private int asteroidCount = 100;       // Number of asteroids to spawn
    [SerializeField] private Vector2 scaleRange = new Vector2(0.5f, 3f); // Min and max scale
    [SerializeField] private float fieldRadius = 50f;       // Radius of the spawn field
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

        // Get a random point biased towards the center
        Vector3 randomPosition = GetBiasedPosition();

        // Pick a random asteroid prefab
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Instantiate asteroid
        GameObject asteroid = Instantiate(asteroidPrefab, randomPosition, Random.rotation, transform);

        // Adjust scale: larger near center, smaller near edges
        float distanceFromCenter = randomPosition.magnitude / fieldRadius;
        float scaleMultiplier = Mathf.Lerp(scaleRange.y, scaleRange.x, distanceFromCenter);
        asteroid.transform.localScale = Vector3.one * scaleMultiplier;
    }

    private Vector3 GetBiasedPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized; // Random direction
        float randomDistance = Mathf.Pow(Random.value, centerBias) * fieldRadius; // Bias towards center
        return transform.position + randomDirection * randomDistance;
    }
}
