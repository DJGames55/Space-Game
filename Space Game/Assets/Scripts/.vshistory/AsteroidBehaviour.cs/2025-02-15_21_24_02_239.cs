using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private bool isCulled = false; // Track if the asteroid is culled

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
        rb.linearVelocity = transform.forward * movementSpeed;
    }

    // When asteroid is out of camera view, stop physics & movement
    private void OnBecameInvisible()
    {
        isCulled = true;
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // Disable physics simulation
        }
    }

    // When asteroid is visible again, resume physics & movement
    private void OnBecameVisible()
    {
        isCulled = false;
        if (rb != null)
        {
            rb.isKinematic = false; // Re-enable physics
            rb.linearVelocity = transform.forward * movementSpeed;
        }
    }
}
