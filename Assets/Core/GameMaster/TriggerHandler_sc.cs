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
            default:
                throw new ArgumentOutOfRangeException(nameof(t), t, null);
        }
        TriggerNextEventInQueue();
     }

    public static void BindToEvent(EffectTriggerHandler_sc c, Trigger t)
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
            ef.TriggerEffect();
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
    public List<EffectTriggerHandler_sc> list = new();
}
