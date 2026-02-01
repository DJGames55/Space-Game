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
                
            }
            else
            {
                
            }
        }
    }
}
