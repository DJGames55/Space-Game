using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlider : MonoBehaviour
{
    public GameObject layoutBox;
    public Image section;
    public TextMeshProUGUI sliderText;

    public void SetLayout(int boxNum)
    {
        foreach (Transform child in layoutBox.transform)
        {
            if (child != section)
                Destroy(child);
        }

        for (int i=0; i<boxNum; i++)
        {
            Instantiate(section, layoutBox.transform);
        }
    }
}
