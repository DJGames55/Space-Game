using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlider : MonoBehaviour
{
    public Slider slider;
    public Image sliderBackground;
    public TextMeshProUGUI sliderText;

    public void SetupSlider()
    {

    }

    public void ValueChange(int sliderValue)
    {
        slider.value = sliderValue;
    }
}
