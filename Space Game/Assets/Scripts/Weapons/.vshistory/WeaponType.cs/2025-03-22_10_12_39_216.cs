using UnityEngine;

[System.Serializable]
public class WeaponType
{
    public GameObject Weapon;
    public string WeaponName;
    public int maxShots;

    private void Awake()
    {
        if (WeaponName == "Lazer")
            maxShots = Weapon.GetComponent<Lazer>().maxShots;
    }
}
