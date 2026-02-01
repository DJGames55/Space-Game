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
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0, 60));
            int trigger = Random.Range(0, 4);
            if (trigger == 0)
            {
                beamAnimator.SetTrigger("Beam");
            }
            else if (trigger == 1)
            {
                beamAnimator.SetTrigger("BeamLeft");
            }
            else if (trigger == 2)
            {
                beamAnimator.SetTrigger("BeamDiagonal");
            }
            else if (trigger == 3)
            {
                beamAnimator.SetTrigger("BeamDiagonal2");
            }
            
            yield return new WaitForSeconds(20);
        }
    }
}
