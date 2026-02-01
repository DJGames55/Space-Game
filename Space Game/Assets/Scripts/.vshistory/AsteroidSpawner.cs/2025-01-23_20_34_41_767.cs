using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private GameObject[] asteroidPrefabs;  // Array of asteroid prefabs
    [SerializeField] private int asteroidCount = 100;       // Number of asteroids to spawn
    [SerializeField] private Vector2 scaleRange = new Vector2(0.5f, 3f); // Min and max scale
    [SerializeField] private float fieldRadius = 50f;       // Outer radius of the asteroid field
    [SerializeField] private float densityPeakRadius = 20f; // The ring where density is highest
    [SerializeField] private float densityFalloff = 10f;    // Controls how density fades at edges

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

        // Get a better spread of positions
        Vector3 randomPosition = GetPositionWithFalloff();

        // Pick a random asteroid prefab
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Instantiate asteroid
        GameObject asteroid = Instantiate(asteroidPrefab, randomPosition, Random.rotation, transform);

        // Adjust scale: larger near the peak density area, smaller near edges
        float distanceFromCenter = Vector3.Distance(randomPosition, transform.position);
        float scaleMultiplier = Mathf.Lerp(scaleRange.y, scaleRange.x, distanceFromCenter / fieldRadius);
        asteroid.transform.localScale = Vector3.one * scaleMultiplier;
    }

    private Vector3 GetPositionWithFalloff()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;

        // Generate a distance with a Gaussian-like distribution
        float randomDistance = GenerateDensityBiasedDistance();

        return transform.position + randomDirection * randomDistance;
    }

    private float GenerateDensityBiasedDistance()
    {
        float distance;
        do
        {
            distance = Random.Range(0f, fieldRadius); // Start with a random distance
            float densityFactor = Mathf.Exp(-Mathf.Pow((distance - densityPeakRadius) / densityFalloff, 2)); // Gaussian-like falloff
            if (Random.value < densityFactor) return distance; // Accept this distance with probability based on density factor
        } while (true);
    }
}
