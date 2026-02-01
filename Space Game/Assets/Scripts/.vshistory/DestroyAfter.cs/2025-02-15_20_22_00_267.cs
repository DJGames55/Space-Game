using System.Collections;
using System.Threading;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float Timer;

    private void OnEnable()
    {
        StartCoroutine(DestoryAfterTime());
    }

    private IEnumerator DestoryAfterTime()
    {
        yield return new WaitForSeconds(Timer);
    }
}
