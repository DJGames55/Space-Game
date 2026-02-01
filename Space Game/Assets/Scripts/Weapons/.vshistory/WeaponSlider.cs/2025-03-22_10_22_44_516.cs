using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlider : MonoBehaviour
{
    public GameObject LayoutBox;
    public Image section;
    public TextMeshProUGUI sliderText;

    public void SetLayout(int boxNum)
    {
        foreach (Transform child in LayoutBox.transform)
        {
            if (child != section)
                Destroy(child);
        }

        for (int i=0; i<boxNum; i++)
        {
            Instantiate(section);
        }
    }
}
