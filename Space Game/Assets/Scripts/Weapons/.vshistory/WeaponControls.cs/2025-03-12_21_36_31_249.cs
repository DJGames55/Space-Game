using Unity.VisualScripting;
using UnityEngine;

public class WeaponControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] protected ShipControls _ship;
    public WeaponType[] weaponTypes;
    public Sound[] WeaponSounds;
    private AudioSource WeaponAudio;

    public bool WeaponsPowered;
    public int WeaponName;

    private void Start()
    {
        _input.OpenWeaponsEvent += PowerWeapons;

        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void PowerWeapons()
    {
        if (WeaponsPowered)
        {
            WeaponsPowered = false;
            Debug.Log("weapons on");
        }
        else
        {
            WeaponsPowered = true;
        }   
    }

    public void ShootWeapon()
    {
        foreach (WeaponType weapon in weaponTypes)
        {
            switch (weapon.WeaponName)
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
            } }
    }

    private void Lazer()
    {

    }

    private void TractorBeam()
    {

    }

    private void Boost()
    {

    }
}
