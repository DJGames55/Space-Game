using UnityEngine;

[System.Serializable]
public class WeaponSlot : MonoBehaviour
{
    public int slotNumber;
    public WeaponType currentWeapon;
    public int maxShots;
    public string weaponType;
    public GameObject numberObject;

    public GameObject box;
    public GameObject boxSelected;

    public void SelectSlot(bool selected)
    {
        if (selected)
        {
            box.gameObject.SetActive(false);
            boxSelected.gameObject.SetActive(true);
        }
        else if (!selected)
        {
            box.gameObject.SetActive(true);
            boxSelected.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        if (currentWeapon.WeaponName == "Lazer")
            maxShots = currentWeapon.Weapon.GetComponent<Lazer>().maxShots;
    }
}
