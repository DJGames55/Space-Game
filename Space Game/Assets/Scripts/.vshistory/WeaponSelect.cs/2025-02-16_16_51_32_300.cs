using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    public bool WeaponsPowered;


    public void PowerWeapons()
    {
        if (WeaponsPowered)
        {
            WeaponsPowered = false;
        }
        else
        {
            WeaponsPowered = true;
        }   
    }
}
