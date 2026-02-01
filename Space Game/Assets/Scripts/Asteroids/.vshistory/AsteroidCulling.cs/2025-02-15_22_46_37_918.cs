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
        if (asteroidRenderer != null)
        {
            if (asteroidRenderer.isVisible)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
