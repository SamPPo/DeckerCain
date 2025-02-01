using Decker;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_sc : MonoBehaviour
{
    [SerializeField]
    Slider healthFill;
    [SerializeField]
    Slider healthDecay;

    public void InitializeSliders(int i)
    {
        healthFill.maxValue = i;
        healthFill.value = i;
        healthDecay.maxValue = i;
        healthDecay.value = i;
    }

    public void SetValue(int i)
    {
        healthFill.value = i;
        StartCoroutine(WaitForDecay(i));
    }

    IEnumerator WaitForDecay(int i)
    {
        yield return new WaitForSeconds(Pvsc.GetWaitTime(WaitTime.Medium));
        healthDecay.value = i;
    }
}
