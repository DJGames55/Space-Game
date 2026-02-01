using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.1f;  // Minimum ambient movement speed
    [SerializeField] private float maxSpeed = 1.0f;  // Maximum ambient movement speed
    [SerializeField] private Rigidbody rb;           // Rigidbody for physics
    private Renderer asteroidRenderer;              // Renderer to check visibility

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        asteroidRenderer = GetComponent<Renderer>(); // Get the Renderer component

        // Set a random movement direction and speed
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        rb.velocity = randomDirection * randomSpeed;
    }

    private void FixedUpdate()
    {
        if (asteroidRenderer != null)
        {
            if (asteroidRenderer.isVisible)
            {
                EnableAsteroid();
            }
            else
            {
                DisableAsteroid();
            }
        }
    }

    private void EnableAsteroid()
    {
        if (!rb.isKinematic)
            return;

        rb.isKinematic = false;  // Re-enable physics
        rb.velocity = Random.insideUnitSphere.normalized * Random.Range(minSpeed, maxSpeed); // Resume movement
    }

    private void DisableAsteroid()
    {
        if (rb.isKinematic)
            return;

        rb.isKinematic = true;  // Disable physics when off-screen
        rb.velocity = Vector3.zero; // Stop movement
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!asteroidRenderer.isVisible) return;  // Ignore collisions if not visible

        // Example: If hit by a bullet, destroy asteroid
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
