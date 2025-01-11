using System.Collections;
using UnityEngine;

public static class Wait
{
    public static Waiter_sc w = null;
}

public class Waiter_sc : MonoBehaviour
{
    void Start()
    { Wait.w = this; }

    public delegate void WaitEnded();
    public static WaitEnded waitEnded;

    public void StartWait(float s)
    {
        StartCoroutine(WaitForDuration(s));
    }

    IEnumerator WaitForDuration(float s)
    {
        yield return new WaitForSeconds(s);
        waitEnded?.Invoke();
    }
}
