using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.1f;     // Minimum ambient movement speed
    [SerializeField] private float maxSpeed = 1.0f;     // Maximum ambient movement speed
    [SerializeField] private Rigidbody rb;              // Rigidbody for physics
    [SerializeField] private Renderer asteroidRenderer; // Renderer to check visibility

    private void Start()
    {
        if (rb == null) { rb = GetComponent<Rigidbody>(); }


        // Set a random movement direction and speed
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        rb.linearVelocity = randomDirection * randomSpeed;
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
        // Set the object active first
        gameObject.SetActive(true);

        // Now wake up the Rigidbody and re-enable physics
        rb.WakeUp();
        rb.isKinematic = false; // Ensure physics is enabled
    }

    private void DisableAsteroid()
    {
        // Put the Rigidbody to sleep and disable physics
        rb.Sleep();
        rb.isKinematic = true; // Disable physics by setting kinematic to true

        // Disable the object
        ///gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!asteroidRenderer.isVisible) return;  // Ignore collisions if not visible

        // Example: If hit by a bullet, destroy asteroid
        //if (collision.gameObject.CompareTag("Bullet"))
        {
            //Destroy(gameObject);
        }
    }
}
