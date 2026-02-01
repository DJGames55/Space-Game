using UnityEngine;
using System.Collections;

public class AsteroidBehaviour : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private int maxHealth = 3;       // Asteroid health before destruction
    [SerializeField] private GameObject debrisPrefab; // Prefab for asteroid debris when destroyed
    [SerializeField] private ParticleSystem impactEffect; // Particle effect on collision
    [SerializeField] private float minAmbientSpeed = 0.5f; // Minimum ambient movement speed
    [SerializeField] private float maxAmbientSpeed = 2f;   // Maximum ambient movement speed
    [SerializeField] private float explosionForce = 5f;    // Force applied to debris

    private int currentHealth;
    private Rigidbody rb;

    private Vector3 linearV;
    private Vector3 angularV;
    private Renderer asteroidRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        asteroidRenderer = GetComponent<Renderer>();

        // Random ambient speed between min and max
        float ambientSpeed = Random.Range(minAmbientSpeed, maxAmbientSpeed);

        // Apply slow random ambient movement
        linearV = Random.insideUnitSphere * ambientSpeed;
        angularV = Random.insideUnitSphere * ambientSpeed * 0.5f;

        rb.linearVelocity = linearV;
        rb.angularVelocity = angularV;
    }

    private void FixedUpdate()
    {
        if (asteroidRenderer != null) 
        {
            if (asteroidRenderer.isVisible && rb.isKinematic)
            {
                EnableAsteroid();
            }
            else if (!asteroidRenderer.isVisible && !rb.isKinematic)
            {
                DisableAsteroid();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Instantiate the impact effect
            ParticleSystem impact = Instantiate(impactEffect, transform.position, Quaternion.identity);

            TakeDamage(25);

            // Destroy the particle system after 2 seconds
            Destroy(impact.gameObject, 2f);
        }
    }

    public void DisableAsteroid()
    {
        linearV = rb.linearVelocity;
        angularV = rb.angularVelocity;
        rb.isKinematic = true;
    }

    public void EnableAsteroid()
    {
        rb.isKinematic = false;
        rb.linearVelocity = linearV;
        rb.angularVelocity = angularV;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            BreakApart();
    }

    private void BreakApart()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject debris = Instantiate(debrisPrefab, transform.position, Random.rotation);
            Rigidbody debrisRb = debris.GetComponent<Rigidbody>();

            if (debrisRb != null)
            {
                debrisRb.linearVelocity = Random.insideUnitSphere * explosionForce;
            }
        }

        Destroy(gameObject);
    }
}
