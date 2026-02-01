using System.Collections;
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
    private int sectionCount;
    private int currentNum;

    public void SetLayout(int boxNum)
    {
        foreach (Transform child in layoutBox.transform)
        {
            Destroy(child.gameObject);
        }

        sections.Clear();
        sectionCount = 0;

        for (int i=0; i<boxNum; i++)
        {
            GameObject newSection = Instantiate(section, layoutBox.transform);
            newSection.SetActive(true)
            newSection.GetComponent<Image>().color = sectionColor;

            var data = new Section
            {
                sectionNum = i,
                section = newSection,
            };

            sections.Add(data);
            sectionCount++;
        }
    }

    public void ValueChange(int value)
    {
        if (value < currentNum)
        {    
            foreach (Section section in sections)
            {
                if (section.sectionNum <= value)
                {
                    section.section.GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
        else if (value > currentNum)
        {
            foreach (Section section in sections)
            {
                if (section.sectionNum >= value)
                {
                    StartCoroutine(Fade(section.section, 0, 1, 0.2f));
                }
            }
        }

        currentNum = value;
    }

    private IEnumerator Fade(GameObject uiObject, float start, float end, float duration)
    {
        float counter = 0f;
        CanvasGroup canvasGroup = uiObject.GetComponent<CanvasGroup>();

        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }
}

public class Section
{
    public int sectionNum;
    public GameObject section;
}
