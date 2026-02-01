using UnityEngine;

public class AsteroidCulling : MonoBehaviour
{
    private Renderer asteroidRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
