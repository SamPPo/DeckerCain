using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EffectContainer_sc : MonoBehaviour
{
    public delegate void EventTriggered();
    public EventTriggered eventTriggerComplete;

    private List<Effect_sc> effects = new();
    private Character_sc owner;

    public void TriggerThis()
    {

    }
}
