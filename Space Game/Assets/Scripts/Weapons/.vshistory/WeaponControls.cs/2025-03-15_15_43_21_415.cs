using UnityEngine;
using UnityEngine.Audio;

public class WeaponControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIManager _ui;
    [SerializeField] private ShipControls _ship;
    public WeaponType currentWeapon;
    public Sound[] WeaponSounds;
    public AudioMixerGroup SFX;
    private AudioSource WeaponAudio;

    public GameObject weaponSelect;
    public WeaponSlot[] weaponSlots;

    public bool WeaponsPowered;
    public int WeaponName;

    private void Start()
    {
        _input.OpenWeaponsEvent += PowerWeapons;

        gameObject.GetComponent<CanvasGroup>().alpha = 0f;

        _input.WSlotSelect1Event += WSlot1;
        _input.WSlotSelect2Event += WSlot2;
        _input.WSlotSelect3Event += WSlot3;

        _input.WeaponActionEvent += WeaponAction;
    }

    private void Awake()
    {
        WeaponAudio = gameObject.AddComponent<AudioSource>();
        WeaponAudio.outputAudioMixerGroup = SFX;
    }

    public void PowerWeapons()
    {
        if (WeaponsPowered)
        {
            WeaponsPowered = false;

            foreach (WeaponSlot weaponSlot in weaponSlots)
                weaponSlot.numberObject.gameObject.SetActive(false);

            StartCoroutine(_ui.Fade(gameObject, 1, 0, 1f));
            return;
        }


        foreach (WeaponSlot weaponSlot in weaponSlots)
            weaponSlot.numberObject.gameObject.SetActive(true);

        WeaponsPowered = true;
        StartCoroutine(_ui.Fade(gameObject, 0, 1, 1f));
    }

    public void Pause(bool pausing)
    {
        if (WeaponsPowered && pausing)
        {
            StartCoroutine(_ui.Fade(gameObject, 1, 0.25f, 0.2f));

            foreach (WeaponSlot weaponSlot in weaponSlots)
                weaponSlot.numberObject.gameObject.SetActive(false);
        }
        else if (WeaponsPowered && !pausing)
        {
            StartCoroutine(_ui.Fade(gameObject, 0.25f, 1, 0.2f));

            foreach (WeaponSlot weaponSlot in weaponSlots)
                weaponSlot.numberObject.gameObject.SetActive(true);
        }
    }

    private void WSlot1()
    {
        foreach (WeaponSlot weaponSlot in weaponSlots)
        {
            if (weaponSlot.slotNumber == 1)
            {
                currentWeapon = weaponSlot.currentWeapon;
                weaponSlot.SelectSlot(true);
            }
            else
                weaponSlot.SelectSlot(false);
        }
    }

    private void WSlot2()
    {
        foreach (WeaponSlot weaponSlot in weaponSlots)
        {
            if (weaponSlot.slotNumber == 2)
            {
                currentWeapon = weaponSlot.currentWeapon;
                weaponSlot.SelectSlot(true);
            }
            else
                weaponSlot.SelectSlot(false);
        }
    }

    private void WSlot3()
    {
        foreach (WeaponSlot weaponSlot in weaponSlots)
        {
            if (weaponSlot.slotNumber == 3)
            {
                currentWeapon = weaponSlot.currentWeapon;
                weaponSlot.SelectSlot(true);
            }
            else
                weaponSlot.SelectSlot(false);
        }
    }


    public void WeaponAction()
    {
        switch (currentWeapon.WeaponName)
        {
            case "Lazer":
                Lazer();
                break;
            case "TractorBeam":
                TractorBeam();
                break;
            case "Boost":
                Boost();
                break;
        }
    }

    private void Lazer()
    {
        Debug.Log("Lazer");
        currentWeapon.Weapon.GetComponent<Lazer>().Shoot();

        Sound s = WeaponSounds[UnityEngine.Random.Range(0, WeaponSounds.Length)];
        WeaponAudio.clip = s.clip;
        WeaponAudio.volume = s.volume;
        WeaponAudio.pitch = s.pitch;

        s.source = WeaponAudio;
        s.source.Play();
    }

    private void TractorBeam()
    {

    }

    private void Boost()
    {
        
    }
}
