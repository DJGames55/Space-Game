using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlider : MonoBehaviour
{
    public Slider slider;
    public Image sliderBackground;
    public TextMeshProUGUI sliderText;

    public CanvasGroup[] sections; // Assign CanvasGroups for each section
    private int totalSections;

    void Start()
    {
        totalSections = sections.Length;
        slider.onValueChanged.AddListener(UpdateSections);
        UpdateSections(slider.value); // Initialize on start
    }

    void UpdateSections(float value)
    {
        float step = 1f / totalSections; // Define section size
        for (int i = 0; i < totalSections; i++)
        {
            float min = i * step;
            float max = (i + 1) * step;

            if (value >= min && value <= max)
            {
                sections[i].alpha = Mathf.Lerp(0, 1, (value - min) / step);
            }
            else
            {
                sections[i].alpha = 0;
            }
        }
    }
}
