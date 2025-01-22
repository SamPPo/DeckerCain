using Decker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectLogic_SO", menuName = "EffectLogic_SO")]
public class EffectLogic_SO : ScriptableObject
{
    public delegate void EffectEnd();
    public static event EffectEnd effectEnd;

    protected string text; public string Text => text;
    protected GameObject ownerCharacter; public GameObject GetOwner() { return ownerCharacter; }
    protected EffectContainer_SO ownerContainer;
    protected int magnitude;
    protected Targetting target;
    protected List<Card_SO> newCards;
    protected WaitTime triggerWaitTime = WaitTime.Short;
    protected List<GameObject> targets;

    private EffectTriggerHandler_sc effectTriggerHandler;

    public Trigger thisTriggers;

    private bool wasCardActivated = false;
    private int i;

    public void InitializeEffect(EffectData e)
    {
        magnitude = e.magn;
        target = e.targ;
        newCards = e.newc;
        triggerWaitTime = e.wait;
        ownerCharacter = e.ownerCharacter;
        ownerContainer = e.ownerContainer;
        effectTriggerHandler = new();
        effectTriggerHandler.InitializeTriggerHandler(e.trig, e.trigT, this);
        SetText();
    }

    protected virtual void SetText()
    {
        text = "No text set";
    }

    protected List<GameObject> GetTargets()
    {
        return ownerCharacter.GetComponent<Targetting_sc>().GetTargets(target);
    }

    public void ActivateEffect(bool cardActivation = false)
    {
        wasCardActivated = cardActivation;
        PrepareEffect();
    }

    private void PrepareEffect()
    {
        targets = GetTargets();
        if (targets.Any())
            PrePlayEffect();
        else
        {
            if (wasCardActivated)
                effectEnd?.Invoke();
        }
    }

    private void PrePlayEffect()
    {
        Waiter_sc.waitEnded += PlayEffect;
        Wait.w.StartWait(Pvsc.GetWaitTime(triggerWaitTime));
        PrePlayEffectInherited();
    }

    private void PlayEffect()
    {
        Waiter_sc.waitEnded -= PlayEffect;
        i = 0;
        LoopEffect();
    }

    private void LoopEffect()
    {
        TriggerHandler.allEventsTriggered -= LoopEffect;
        if (i < targets.Count)
        {
            var target = targets[i];
            i++;
            PlayEffectInherited(i, target);
            TriggerHandler.allEventsTriggered += LoopEffect;
            TriggerHandler.TriggerEvent(thisTriggers, ownerCharacter, target);
        }
        else 
        {
            if (wasCardActivated)
                effectEnd?.Invoke();
        }
    }

    protected virtual void PrePlayEffectInherited()
    {

    }

    protected virtual void PlayEffectInherited(int loopCount, GameObject target)
    {

    }

    public void BindTriggers()
    {
        effectTriggerHandler?.BindTrigger();
    }
}
