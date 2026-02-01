using UnityEngine;

public class AsteroidCulling : MonoBehaviour
{
    private Renderer asteroidRenderer;

    private void OnEnable()
    {
        asteroidRenderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (asteroidRenderer == null) return;

        bool isVisible = asteroidRenderer.isVisible;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isVisible); // Enable children when visible, disable when off-screen
        }
    }
}
