using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Lazer : MonoBehaviour
{
    public GameObject Projectile;
    public float ProjectileSpeed;
    private bool CanShoot;
    public float WeaponCooldownTime;

    [Header("Audio")]
    public Sound[] WeaponSounds;
    private AudioSource WeaponAudio;
    [SerializeField] private AudioMixerGroup SFX;

    private void Awake()
    {
        WeaponAudio = gameObject.AddComponent<AudioSource>();
        WeaponAudio.outputAudioMixerGroup = SFX;
    }

    public void Shoot()
    {
        if (!CanShoot)
            return;

        Sound s = WeaponSounds[UnityEngine.Random.Range(0, WeaponSounds.Length)];
        WeaponAudio.clip = s.clip;
        WeaponAudio.volume = s.volume;
        WeaponAudio.pitch = s.pitch;

        s.source = WeaponAudio;
        s.source.Play();
        StartCoroutine(WeaponCooldown(WeaponCooldownTime));

        if (Projectile == null)
            return;

        GameObject newProjectile = Instantiate(Projectile, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        
        if (rb != null)
            rb.linearVelocity = gameObject.transform.forward * ProjectileSpeed;
    }

    private IEnumerator WeaponCooldown(float time)
    {
        CanShoot = false;
        yield return new WaitForSeconds(time);
        CanShoot = true;
    }
}
