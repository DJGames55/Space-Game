using UnityEngine;
using System.Collections;

public class AsteroidBehaviour : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private int maxHealth = 3;       // Asteroid health before destruction
    [SerializeField] private GameObject debrisPrefab; // Prefab for asteroid debris when destroyed
    [SerializeField] private GameObject oreDrop; // Prefab for when Ore asteroid is destoyed
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

        maxHealth = (int)Mathf.Round(maxHealth * transform.localScale.x);
        rb.isKinematic = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Instantiate the impact effect
            ParticleSystem impact = Instantiate(impactEffect, transform.position, Quaternion.identity);

            TakeDamage(25);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Lazer"))
        {
            ParticleSystem impact = Instantiate(impactEffect, collision.transform.position, Quaternion.identity);
            impact.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

            Destroy(collision.gameObject);

            TakeDamage(10);
        }
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
            Vector3 spawnOffset = Random.insideUnitSphere * 0.5f; // Small random offset
            GameObject debris = Instantiate(debrisPrefab, transform.position + spawnOffset, Random.rotation);
            Rigidbody debrisRb = debris.GetComponent<Rigidbody>();

            if (debrisRb != null)
                debrisRb.linearVelocity = Random.insideUnitSphere * explosionForce; // Use velocity, not linearVelocity
        }

        if (oreDrop != null)
        {
            int OreRange = Random.Range(oreDrop.GetComponent<OreBehaviour>().OreNumberMin, oreDrop.GetComponent<OreBehaviour>().OreNumberMax);

            for (int i = 0; i < OreRange; i++)
            {
                Vector3 spawnOffset = Random.insideUnitSphere * 0.5f; // Avoid overlapping ores
                GameObject ore = Instantiate(oreDrop, transform.position + spawnOffset, Random.rotation);
            }

        }

        Destroy(gameObject);
    }
}
