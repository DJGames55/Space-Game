using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    public bool WeaponsPowered;

    private void Start()
    {
        _input.OpenWeaponsEvent += PowerWeapons;
    }

    public void PowerWeapons()
    {
        if (WeaponsPowered)
        {
            WeaponsPowered = false;
            Debug.Log("weaposn on");
        }
        else
        {
            WeaponsPowered = true;
        }   
    }
}
