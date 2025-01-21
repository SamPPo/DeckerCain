using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Decker;
using System;
using System.Linq;

public static class TriggerHandler
{
    public delegate void StartRoundDelegate();
    public static StartRoundDelegate startRound;

    public delegate void OnCardPlayDelegate();
    public static OnCardPlayDelegate onCardPlay;

    public delegate void OnDamageDelegate();
    public static OnDamageDelegate onDamage;

    public delegate void AllEventsTriggeredDelegate();
    public static AllEventsTriggeredDelegate allEventsTriggered;

    public delegate void ResetTriggersDelegate();
    public static ResetTriggersDelegate resetTriggers;

    private static List<TriggerBindList> triggerBindList = new();

    public static void TriggerEvent(Trigger t)
    {
        switch (t)
        {
            case Trigger.none:
                // Do nothing
                break;
            case Trigger.OnCardPlay:
                // Handle OnCardPlay trigger
                onCardPlay?.Invoke();
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
                onDamage?.Invoke();
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
            case Trigger.OnCardAdd:
                // Handle OnMiss trigger
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(t), t, null);
        }
        TriggerNextEventInQueue();
     }

    public static void BindToEvent(EffectLogic_SO c, Trigger t)
    {
        bool added = false;
        Debug.Log(c + "is bound to " + t + " event");
        if (triggerBindList.Count > 0)
        {
            foreach (var a in triggerBindList)
            {
                if (a.type == t)
                {
                    a.list.Add(c);
                    added = true;
                }
            }
        }
        if (!added)
        {
            var tbl = new TriggerBindList();
            tbl.list.Add(c);
            tbl.type = t;
            triggerBindList.Add(tbl);
        }
    }

    private static void TriggerNextEventInQueue()
    {
        if (triggerBindList.Count != 0)
        {
            var ef = triggerBindList.Last().list.Last();
            int i = triggerBindList.Last().list.Count - 1;
            if (i > 0)
            {
                triggerBindList.Last().list.RemoveAt(i);
            }
            else 
            {
                triggerBindList.RemoveAt(triggerBindList.Count - 1);
            }
            ef.ActivateEffect();
        }
        else
        {
            AllEventsTriggered();
        }
    }

    private static void AllEventsTriggered()
    {
        allEventsTriggered?.Invoke();
    }

    public static void ResetAllTriggers()
    {
        resetTriggers?.Invoke();
    }
}

public class TriggerBindList
{
    public Trigger type;
    public List<EffectLogic_SO> list = new();
}
