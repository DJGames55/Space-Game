using UnityEngine;
using System.Collections;

public class AsteroidBehaviour : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private int maxHealth = 3;       // Asteroid health before destruction
    [SerializeField] private GameObject debrisPrefab; // Prefab for asteroid debris when destroyed
    [SerializeField] private ParticleSystem impactEffect; // Particle effect on collision
    [SerializeField] private float ambientSpeed = 1f; // Speed of slow ambient movement
    [SerializeField] private float explosionForce = 5f; // Force applied to debris
    [SerializeField] private float fadeTime = 2f;     // Time before debris fades out

    private int currentHealth;
    private Rigidbody rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

        // Apply slow random ambient movement
        rb.velocity = Random.insideUnitSphere * ambientSpeed;
        rb.angularVelocity = Random.insideUnitSphere * ambientSpeed * 0.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If colliding with another asteroid, play impact effect and reduce health
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            TakeDamage(1);

            // Bounce off with some force
            rb.AddForce(collision.contacts[0].normal * explosionForce, ForceMode.Impulse);
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
        // Spawn debris
        for (int i = 0; i < 3; i++)
        {
            GameObject debris = Instantiate(debrisPrefab, transform.position, Random.rotation);
            Rigidbody debrisRb = debris.GetComponent<Rigidbody>();

            if (debrisRb != null)
            {
                debrisRb.velocity = Random.insideUnitSphere * explosionForce;
            }

            StartCoroutine(FadeAndDestroy(debris));
        }

        // Destroy the main asteroid
        Destroy(gameObject);
    }

    private IEnumerator FadeAndDestroy(GameObject debris)
    {
        Renderer debrisRenderer = debris.GetComponent<Renderer>();
        Color originalColor = debrisRenderer.material.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            debrisRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(debris);
    }
}
