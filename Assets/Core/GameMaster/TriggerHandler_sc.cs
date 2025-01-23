using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Decker;
using System;
using System.Linq;

public static class TriggerHandler
{
    public delegate void OnCombatStartRoundDelegate(GameObject instigator = null, GameObject target = null);
    public static OnCombatStartRoundDelegate onCombatStart;

    public delegate void OnCardPlayDelegate(GameObject instigator = null, GameObject target = null);
    public static OnCardPlayDelegate onCardPlay;

    public delegate void OnDamageDelegate(GameObject instigator = null, GameObject target = null);
    public static OnDamageDelegate onDamage;

    public delegate void AllEventsTriggeredDelegate();
    public static AllEventsTriggeredDelegate allEventsTriggered;

    public delegate void ResetTriggersDelegate();
    public static ResetTriggersDelegate resetTriggers;

    private static List<TriggerBindList> triggerBindList = new();

    public static void TriggerEvent(Trigger t, GameObject instigator = null, GameObject target = null)
    {
        switch (t)
        {
            case Trigger.none:
                // Do nothing
                break;
            case Trigger.OnCardPlay:
                onCardPlay?.Invoke();
                break;
            case Trigger.OnCombatStart:
                onCombatStart?.Invoke();
                break;
            case Trigger.OnCombatEnd:
                break;
            case Trigger.StartOfTurn:
                break;
            case Trigger.EndOfTurn:
                break;
            case Trigger.OnDeath:
                break;
            case Trigger.OnDamage:
                onDamage?.Invoke(instigator, target);
                break;
            case Trigger.OnHeal:
                break;
            case Trigger.OnKill:
                break;
            case Trigger.OnSpend:
                break;
            case Trigger.OnCrit:
                break;
            case Trigger.OnMiss:
                break;
            case Trigger.OnCardAdd:
                break;
            case Trigger.OnShuffleDeck:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(t), t, null);
        }
        TriggerNextEventInQueue();
     }

    public static void BindToEvent(EffectTriggerHandler_sc eth, Trigger t)
    {
        bool added = false;
        Debug.Log(eth + "is bound to " + t + " event");
        if (triggerBindList.Count > 0)
        {
            foreach (var a in triggerBindList)
            {
                if (a.type == t)
                {
                    a.list.Add(eth);
                    added = true;
                }
            }
        }
        if (!added)
        {
            var tbl = new TriggerBindList();
            tbl.list.Add(eth);
            tbl.type = t;
            triggerBindList.Add(tbl);
        }
    }

    public static void UnbindFromHandler(EffectTriggerHandler_sc eth)
    {
        foreach (var a in triggerBindList)
        {
            if (a.list.Contains(eth))
            {
                a.list.Remove(eth);
                if (!a.list.Any())
                {
                    triggerBindList.Remove(a);
                }
                break;
            }
        }
        TriggerNextEventInQueue();
    }

    private static void TriggerNextEventInQueue()
    {
        if (triggerBindList.Any())
        {
            Debug.Log("Triggered next event in queue");
            var ef = triggerBindList.Last().list.Last();
            int i = triggerBindList.Last().list.Count - 1;
            ef.TriggerEffect();  
        }
        else
        {
            Debug.Log("All events triggered");
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
    public List<EffectTriggerHandler_sc> list = new();
}
