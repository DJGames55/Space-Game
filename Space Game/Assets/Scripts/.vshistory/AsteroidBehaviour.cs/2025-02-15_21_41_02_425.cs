using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.1f;  // Minimum ambient movement speed
    [SerializeField] private float maxSpeed = 1.0f;  // Maximum ambient movement speed
    [SerializeField] private Rigidbody rb;           // Rigidbody for physics
    public bool isVisible = false;                 // Tracks if the asteroid is visible

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        // Set a random movement direction and speed
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        rb.linearVelocity = randomDirection * randomSpeed;
    }

    private void OnBecameVisible()
    {
        isVisible = true;
        if (rb != null)
        {
            rb.isKinematic = false;  // Enable physics when visible
        }
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
        if (rb != null)
        {
            rb.isKinematic = true;   // Disable physics when off-screen
            rb.linearVelocity = Vector3.zero; // Stop movement to reduce lag
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isVisible) return;  // Ignore collisions if not visible

        // Example: If hit by a bullet, destroy asteroid
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
