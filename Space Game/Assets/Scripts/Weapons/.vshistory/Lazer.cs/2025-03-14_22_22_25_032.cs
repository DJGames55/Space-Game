using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject Projectile;
    public float ProjectileSpeed;

    public void Shoot()
    {
        if (Projectile == null)
            return;
    }
}
