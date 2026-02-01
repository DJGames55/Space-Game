using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject Projectile;
    public float ProjectileSpeed;

    public void Shoot()
    {
        if (Projectile == null)
            return;

        GameObject newProjectile = Instantiate(Projectile, gameObject.transform.position, gameObject.transform.rotation);
    }
}
