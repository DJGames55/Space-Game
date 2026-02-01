using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlider : MonoBehaviour
{
    public GameObject layoutBox;
    public GameObject section;
    public Color sectionColor;
    public TextMeshProUGUI sliderText;

    public List<Section> sections = new List<Section>();

    public void SetLayout(int boxNum)
    {
        foreach (Transform child in layoutBox.transform)
        {
            if (child !=  section)
                Destroy(child);
        }

        sections.Clear();

        for (int i=0; i<boxNum; i++)
        {
            GameObject newSection = Instantiate(section, layoutBox.transform);
            newSection.GetComponent<Image>().color = sectionColor;

            var data = new Section
            {
                sectionNum = i,
                section = newSection,
                sectionCG = newSection.GetComponent<CanvasGroup>(),
            };

            sections.Add(data);
        }
    }
}

public class Section
{
    public int sectionNum;
    public GameObject section;
    public CanvasGroup sectionCG;
}
