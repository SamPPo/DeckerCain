using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectContainer_sc : MonoBehaviour
{
    public delegate void EventTriggered();
    public EventTriggered eventTriggerComplete;

    private List<EffectLogic_SO> effects = new();
    private Character_sc owner;

    public void TriggerActivation()
    {
        StartCoroutine(WaitAfterActivation());
    }

    IEnumerator WaitAfterActivation()
    {
        yield return new WaitForSeconds(1);
        eventTriggerComplete?.Invoke();
    }
}
