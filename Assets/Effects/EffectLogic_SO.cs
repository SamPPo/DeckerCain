using Decker;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectLogic_SO", menuName = "EffectLogic_SO")]
public class EffectLogic_SO : ScriptableObject
{
    public delegate void EventTriggerCompletedDelegate();
    public static EventTriggerCompletedDelegate eventTriggerCompleted;


    private int magnitude;
    private Targetting target;
    private List<Card_SO> newCards;
    private Trigger trigger;

    public bool hasTriggered;

    public void SetEffectData(int m, Targetting t, List<Card_SO> c, Trigger r)
    {
        magnitude = m;
        target = t;
        newCards = c;
        trigger = r;
        //BindToTriggerDelegates();
    }

    public void PlayEffect()
    {
        Debug.Log("Deal " + magnitude + " damage");
    }

    public void TriggerActivation()
    {
        Debug.Log("TRIGGERED!");
        eventTriggerCompleted?.Invoke();
    }

    public void BindToTriggerDelegates()
    {
        Debug.Log("Bind to " + trigger);
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
        TriggerHandler.BindToEvent(this, trigger);
    }
}
