using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    public bool WeaponsPowered;

    private void Start()
    {
        
    }

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
