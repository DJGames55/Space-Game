using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class MainMenuAnimations : MonoBehaviour
{
    public Animator beamAnimator;

    void Start()
    {
        if (beamAnimator != null)
        {
            StartCoroutine(Beams());
        }
    }

    private IEnumerator Beams()
    {
        WaitForSeconds(2f);
        return null;
    }
}
