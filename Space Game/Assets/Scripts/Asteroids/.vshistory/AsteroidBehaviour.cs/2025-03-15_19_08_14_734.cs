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
    private Rigidbody rigidBody;

    private Vector3 linearV;
    private Vector3 angularV;
    private Renderer asteroidRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        rigidBody = rb;
        asteroidRenderer = GetComponent<Renderer>();

        // Random ambient speed between min and max
        float ambientSpeed = Random.Range(minAmbientSpeed, maxAmbientSpeed);

        // Apply slow random ambient movement
        linearV = Random.insideUnitSphere * ambientSpeed;
        angularV = Random.insideUnitSphere * ambientSpeed * 0.5f;

        rb.linearVelocity = linearV;
        rb.angularVelocity = angularV;

        maxHealth = (int)Mathf.Round(maxHealth * transform.localScale.x);
        DisableAsteroid();
    }

    private void FixedUpdate()
    {
        if (asteroidRenderer != null) 
        {
            if (asteroidRenderer.isVisible && gameObject.isStatic)
            {
                EnableAsteroid();
            }
            else if (!asteroidRenderer.isVisible && !gameObject.isStatic)
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
        }
        else if (collision.gameObject.CompareTag("Lazer"))
        {
            ParticleSystem impact = Instantiate(impactEffect, collision.transform.position, Quaternion.identity);
            impact.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            
            Destroy(collision.gameObject);

            TakeDamage(10);
        }
    }

    public void DisableAsteroid()
    {
        linearV = rb.linearVelocity;
        angularV = rb.angularVelocity;
        rb.isKinematic = true;
        rb.Sleep();
    }

    public void EnableAsteroid()
    {
        rb.isKinematic = false;
        rb.WakeUp();
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
                debrisRb.linearVelocity = Random.insideUnitSphere * explosionForce;
        }
        if (oreDrop != null)
        {
            int OreRange = UnityEngine.Random.Range(0, 10);
            for (int i = 0; i < OreRange; i++)
            {
                GameObject ore = Instantiate(oreDrop, transform.position, Random.rotation);
            }
        }

        Destroy(gameObject);
    }
}
