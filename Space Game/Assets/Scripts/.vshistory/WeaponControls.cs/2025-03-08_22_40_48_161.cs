using UnityEngine;

public class WeaponControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;

    public bool WeaponsPowered;

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
}
