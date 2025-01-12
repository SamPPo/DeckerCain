using Decker;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectLogic_SO", menuName = "EffectLogic_SO")]
public class EffectLogic_SO : ScriptableObject
{
    private GameObject owner;
    private int magnitude;
    private Targetting target;
    private List<Card_SO> newCards;
    private Trigger trigger;
    private WaitTime triggerWaitTime = WaitTime.Short;

    protected Trigger thisTriggers = Trigger.none;

    private bool hasTriggered = false;
    private bool isBound = false;

    public void SetEffectData(EffectData e)
    {
        magnitude = e.magn;
        target = e.targ;
        newCards = e.newc;
        trigger = e.trig;
        triggerWaitTime = e.wait;
        owner = e.owner;
    }

    private List<GameObject> GetTarget()
    {
        return owner.GetComponent<Targetting_sc>().GetTarget(target);
    }

    public void PlayEffect()
    {
        //DEBUG STUFF
        Debug.Log("EffectLogic_SO: " + GetInstanceID() + " Deal " + magnitude + " damage");
        thisTriggers = Trigger.OnDamage;
    }

    public void ActivateEffect()
    {
        hasTriggered = true;
        PlayEffect();
        Waiter_sc.waitEnded += ActivationFinished;
        Wait.w.StartWait(Pvsc.GetWaitTime(triggerWaitTime));
    }

    private void ActivationFinished()
    {
        Waiter_sc.waitEnded -= ActivationFinished;
        TriggerHandler.TriggerEvent(thisTriggers);
    }

    public void BindToTriggerDelegates()
    {
        Debug.Log("EffectLogic_SO: Bind to " + trigger);
        switch (trigger)
        {
            case Trigger.OnCardPlay:
                // Handle OnCardPlay trigger
                TriggerHandler.onCardPlay += BindToTrigger;
                break;
            case Trigger.OnRoundStart:
                // Handle OnRoundStart trigger
                break;
            case Trigger.OnRoundEnd:
                // Handle OnRoundEnd trigger
                break;
            case Trigger.StartOfTurn:
                // Handle StartOfTurn trigger
                break;
            case Trigger.EndOfTurn:
                // Handle EndOfTurn trigger
                break;
            case Trigger.OnPlay:
                // Handle OnPlay trigger
                break;
            case Trigger.OnDraw:
                // Handle OnDraw trigger
                break;
            case Trigger.OnDiscard:
                // Handle OnDiscard trigger
                break;
            case Trigger.OnDeath:
                // Handle OnDeath trigger
                break;
            case Trigger.OnDamage:
                // Handle OnDamage trigger
                TriggerHandler.onDamage += BindToTrigger;
                break;
            case Trigger.OnHeal:
                // Handle OnHeal trigger
                break;
            case Trigger.OnKill:
                // Handle OnKill trigger
                break;
            case Trigger.OnSpend:
                // Handle OnSpend trigger
                break;
            case Trigger.OnCrit:
                // Handle OnCrit trigger
                break;
            case Trigger.OnMiss:
                // Handle OnMiss trigger
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(trigger), trigger, null);
        }
    }

    private void BindToTrigger()
    {
        if (!hasTriggered && !isBound)
        {
            isBound = true;
            TriggerHandler.resetTriggers += ResetTrigger;
            TriggerHandler.BindToEvent(this, trigger);
        }
    }

    public void ResetTrigger()
    {
        TriggerHandler.resetTriggers -= ResetTrigger;
        hasTriggered = false;
        isBound = false;
    }
}
