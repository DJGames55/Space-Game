using Unity.VisualScripting;
using UnityEngine;

public class WeaponControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    public Sound[] WeaponSounds;
    private AudioSource WeaponAudio;

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
