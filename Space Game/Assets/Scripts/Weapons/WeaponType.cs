using UnityEngine;

[System.Serializable]
public class WeaponType
{
    public GameObject Weapon;
    public string WeaponName;
    public int maxShots;

    public Color color;

    private void Awake()
    {
        if (WeaponName == "Laser")
            maxShots = Weapon.GetComponent<Laser>().maxShots;
    }
}
