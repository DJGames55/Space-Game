using UnityEngine;

[System.Serializable]
public class WeaponSlot : MonoBehaviour
{
    public int slotNumber;
    public string currentWeapon;
    public GameObject numberObject;

    public GameObject box;
    public GameObject boxSelected;

    public void SelectSlot(bool selected)
    {
        if (selected)
        {
            box.gameObject.SetActive(false);
            boxSelected.gameObject.SetActive(true)
        }
        else if (!selected)
        {
            box.gameObject.SetActive(true);
            boxSelected.gameObject.SetActive(false)
        }
    }
}
