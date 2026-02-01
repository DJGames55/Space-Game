using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Lazer : MonoBehaviour
{
    [SerializeField] private WeaponSlider slider;
    public GameObject Projectile;
    public float ProjectileSpeed;
    private bool CanShoot = true;
    public float WeaponCooldownTime;
    public int maxShots;
    private int currentShots;
    public float ReloadTime;

    [Header("Audio")]
    public Sound[] WeaponSounds;
    private AudioSource WeaponAudio;
    [SerializeField] private AudioMixerGroup SFX;
    private Coroutine recharge;

    private void Awake()
    {
        WeaponAudio = gameObject.AddComponent<AudioSource>();
        WeaponAudio.outputAudioMixerGroup = SFX;

        currentShots = maxShots;
        recharge = StartCoroutine(WeaponRecharge(ReloadTime, Mathf.RoundToInt(maxShots / 2)));
    }

    public void Shoot()
    {
        if (currentShots == 0)
            return;

        if (!CanShoot)
            return;

        StopCoroutine(recharge);

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

        currentShots--;

        slider.ValueChange(currentShots);
        recharge = StartCoroutine(WeaponRecharge(ReloadTime, Mathf.RoundToInt(maxShots / 2)));
    }

    private IEnumerator WeaponCooldown(float time)
    {
        CanShoot = false;
        yield return new WaitForSeconds(time);
        CanShoot = true;
    }

    private IEnumerator WeaponRecharge(float time, int number)
    {
        if (currentShots < number)
            CanShoot = false;

        while (currentShots != maxShots)
        {
            yield return new WaitForSeconds(time);
        
            currentShots++;
            slider.ValueChange(currentShots);
        }
        yield return null;
    }
}
