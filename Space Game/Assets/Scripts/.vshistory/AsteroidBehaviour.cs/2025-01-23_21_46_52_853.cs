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

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

        // Random ambient speed between min and max
        float ambientSpeed = Random.Range(minAmbientSpeed, maxAmbientSpeed);

        // Apply slow random ambient movement
        rb.linearVelocity = Random.insideUnitSphere * ambientSpeed;
        rb.angularVelocity = Random.insideUnitSphere * ambientSpeed * 0.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Instantiate the impact effect
            ParticleSystem impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        
            TakeDamage(1);
            rb.AddForce(collision.contacts[0].normal * explosionForce, ForceMode.Impulse);

            // Destroy the particle system after 2 seconds
            Destroy(impact.gameObject, 2f); 
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            BreakApart();
        }
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

            StartCoroutine(FadeAndDestroy(debris));
        }

        Destroy(gameObject);
    }

    private IEnumerator FadeAndDestroy(GameObject debris)
    {
        yield return new WaitForSeconds(12);

        Destroy(debris);
    }
}
