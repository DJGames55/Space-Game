using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlider : MonoBehaviour
{
    public GameObject layoutBox;
    public Image section;
    public Color sectionColor;
    public TextMeshProUGUI sliderText;

    public void SetLayout(int boxNum)
    {
        foreach (Transform child in layoutBox.transform)
        {
            Destroy(child);
        }

        for (int i=0; i<boxNum; i++)
        {
            Instantiate(section, layoutBox.transform);
        }
    }
}
