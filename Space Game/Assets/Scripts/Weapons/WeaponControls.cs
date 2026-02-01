using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class WeaponControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIManager _ui;
    [SerializeField] private ShipControls _ship;
    public WeaponType currentWeapon;

    [Header("Weapon Slots")]
    public GameObject weaponSelect;
    public WeaponSlot[] weaponSlots;
    public WeaponSlider WeaponSlider;
    public Image Crosshair;

    public bool WeaponsPowered;
    private bool CanShoot; 

    private void Start()
    {
        _input.OpenWeaponsEvent += PowerWeapons;

        weaponSelect.GetComponent<CanvasGroup>().alpha = 0f;

        _input.WSlotSelect1Event += WSlot1;
        _input.WSlotSelect2Event += WSlot2;
        _input.WSlotSelect3Event += WSlot3;

        _input.WeaponActionEvent += WeaponAction;

        WeaponSlider.gameObject.SetActive(false);
    }

    public void PowerWeapons()
    {
        if (WeaponsPowered)
        {
            WeaponsPowered = false;

            foreach (WeaponSlot weaponSlot in weaponSlots)
                weaponSlot.numberObject.gameObject.SetActive(false);

            StartCoroutine(_ui.Fade(weaponSelect, 1, 0, 1f));
            return;
        }


        foreach (WeaponSlot weaponSlot in weaponSlots)
            weaponSlot.numberObject.gameObject.SetActive(true);

        WeaponsPowered = true;
        Pause(false);
        StartCoroutine(_ui.Fade(weaponSelect, 0, 1, 1f));
    }

    public void Pause(bool pausing)
    {
        if (WeaponsPowered && pausing)
        {
            StartCoroutine(_ui.Fade(weaponSelect, 1, 0.25f, 0.2f));

            foreach (WeaponSlot weaponSlot in weaponSlots)
                weaponSlot.numberObject.gameObject.SetActive(false);

            WeaponSlider.gameObject.SetActive(false);
            Crosshair.gameObject.SetActive(false);
        }
        else if (WeaponsPowered && !pausing)
        {
            StartCoroutine(_ui.Fade(weaponSelect, 0.25f, 1, 0.2f));

            foreach (WeaponSlot weaponSlot in weaponSlots)
                weaponSlot.numberObject.gameObject.SetActive(true);

            WeaponSlider.gameObject.SetActive(true);
            Crosshair.gameObject.SetActive(true);
        }
    }

    private void WSlot1()
    {
        foreach (WeaponSlot weaponSlot in weaponSlots)
        {
            if (weaponSlot.slotNumber == 1)
            {
                currentWeapon = weaponSlot.currentWeapon;
                SetSlider();
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
                SetSlider();
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
                SetSlider();
                weaponSlot.SelectSlot(true);
            }
            else
                weaponSlot.SelectSlot(false);
        }
    }

    private void SetSlider()
    {
        WeaponSlider.gameObject.SetActive(true);
        WeaponSlider.sliderText.text = currentWeapon.WeaponName;

        switch (currentWeapon.WeaponName)
        {
            case "Laser":
                WeaponSlider.slider.maxValue = currentWeapon.Weapon.GetComponent<Laser>().maxShots;
                WeaponSlider.slider.value = currentWeapon.Weapon.GetComponent<Laser>().currentShots;
                break;
            case "TractorBeam":
                // Tractor Beam Duration
                break;
            case "Boost":
                // Boost Duration
                break;
        }
    }

    public void WeaponAction()
    {
        if (WeaponsPowered)
        {
            switch (currentWeapon.WeaponName)
            {
                case "Laser":
                    Laser();
                    break;
                case "TractorBeam":
                    TractorBeam();
                    break;
                case "Boost":
                    Boost();
                    break;
            }
        }
    }

    private void Laser()
    {
        currentWeapon.Weapon.GetComponent<Laser>().Shoot();
    }

    private void TractorBeam()
    {

    }

    private void Boost()
    {
        
    }
}
