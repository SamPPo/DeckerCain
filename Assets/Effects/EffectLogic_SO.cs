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
    protected string text; public string Text => text;
    protected GameObject ownerCharacter; public GameObject GetOwner() { return ownerCharacter; }
    protected EffectContainer_SO ownerContainer;
    protected EffectType effectType;
    protected int magnitude;
    private int activationCount;
    protected int activationCountDelta;
    protected Targetting target;
    protected List<Card_SO> newCards;
    protected WaitTime triggerWaitTime = WaitTime.Short;
    protected List<GameObject> targets;

    private EffectTriggerHandler_sc effectTriggerHandler;

    public Trigger thisTriggers;

    private int i;
    private int j;
    private bool isActive = false;

    public void InitializeEffect(EffectData e)
    {
        magnitude = e.magn;
        activationCount = e.aCount;
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

    public void ActivateEffect()
    {
        if (isActive)
        {
            LoopEffect();
        }
        else
        {
            isActive = true;
            PrepareEffect();
        } 
    }

    private void PrepareEffect()
    {
        //ModifiableEffectData effectData = new()
        //{
        //    magnitude = magnitude,
        //    activationCount = activationCount,
        //    target = target
        //};
        targets = GetTargets();
        if (targets.Any())
            PrePlayEffect();
        else
        {
            EndEffectActivation();
        }
    }

    private void PrePlayEffect()
    {
        activationCountDelta = activationCount;
        Waiter_sc.waitEnded += PlayEffect;
        Wait.w.StartWait(Pvsc.GetWaitTime(triggerWaitTime));
        PrePlayEffectInherited();
    }

    private void PlayEffect()
    {
        i = 0;
        j = 0;
        Waiter_sc.waitEnded -= PlayEffect;
        LoopEffect();
    }

    private void LoopEffect()
    {
        if (j < activationCountDelta)
        {
            var target = targets[i];
            PlayEffectInherited(j, target);
            j++;
            TriggerHandler.TriggerEvent(thisTriggers, ownerCharacter, target);
        }
        else
        {
            j = 0;
            i++;
            if (i < targets.Count)
            {
                var target = targets[i];
                PlayEffectInherited(j, target);
                j++;
                TriggerHandler.TriggerEvent(thisTriggers, ownerCharacter, target);
            }
            else
            {
                EndEffectActivation();
            }

        }
    }

    private void EndEffectActivation()
    {
        isActive = false;
        effectTriggerHandler?.UnbindTrigger();
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
