using UnityEngine;

[ExecuteInEditMode] // Allows script to update outside of Play mode
public class AsteroidSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField, Range(1, 100)] private int asteroidCount = 10; // Creates a slider
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private Collider spawnArea;
    [SerializeField] private float minSpawnDistance = 4f;

    private void OnValidate()
    {
        if (!Application.isPlaying) // Prevent updates during play mode
        {
            GenerateAsteroidsInEditor();
        }
    }

    private void GenerateAsteroidsInEditor()
    {
        ClearAsteroids();

        for (int i = 0; i < asteroidCount; i++)
        {
            Vector3 spawnPoint = GetRandomPointInCollider();
            GameObject selectedPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
            GameObject newAsteroid = Instantiate(selectedPrefab, spawnPoint, Quaternion.identity, transform);
            newAsteroid.transform.localScale = Vector3.one * Random.Range(0.5f, 2f);
        }
    }

    private void ClearAsteroids()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private Vector3 GetRandomPointInCollider()
    {
        Bounds bounds = spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
