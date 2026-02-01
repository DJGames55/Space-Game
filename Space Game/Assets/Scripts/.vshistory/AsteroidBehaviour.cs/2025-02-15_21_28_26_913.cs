using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private bool isCulled = false;

    [Header("Asteroid Settings")]
    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 rotationAxis;
    private float movementSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Set random movement and rotation
        movementSpeed = Random.Range(minSpeed, maxSpeed);
        rotationAxis = Random.onUnitSphere * rotationSpeed;
    }

    private void FixedUpdate()
    {
        if (isCulled) return; // Stop movement when culled

        // Apply rotation
        transform.Rotate(rotationAxis * Time.fixedDeltaTime);

        // Move asteroid forward
        rb.velocity = transform.forward * movementSpeed;
    }

    // Stop physics & disable components when culled
    private void OnBecameInvisible()
    {
        isCulled = true;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // Stop physics calculations
            rb.detectCollisions = false; // Disable collision detection
        }

        // Disable script to stop all updates
        enabled = false;
    }

    // Resume physics when visible again
    private void OnBecameVisible()
    {
        isCulled = false;

        if (rb != null)
        {
            rb.isKinematic = false; // Resume physics
            rb.detectCollisions = true; // Enable collision detection
            rb.velocity = transform.forward * movementSpeed;
        }

        // Re-enable script
        enabled = true;
    }
}
