using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlider : MonoBehaviour
{
    public GameObject layoutBox;
    public GameObject section;
    public Color sectionColor;
    public TextMeshProUGUI sliderText;

    public void SetLayout(int boxNum)
    {
        foreach (Transform child in layoutBox.transform)
        {
            if (child !=  section)
                Destroy(child);
        }

        for (int i=0; i<boxNum; i++)
        {
            GameObject newSection = Instantiate(section, layoutBox.transform);
            newSection.GetComponent<Image>().color = sectionColor;
        }
    }
}
