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

    public delegate void TriggersDoneDelegate();
    public static TriggersDoneDelegate triggersDone;

    private static List<TriggerBindList> triggerBindList = new();
    private static List<EffectLogic_SO> binders = new();
    private static int listIndex;
    private static int innerIndex;

    public static void EventTrigger(Trigger t)
    {
        switch (t)
        {
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
                throw new ArgumentOutOfRangeException(nameof(t), t, null);
        }

        if (binders.Count > 0) 
        {
            Debug.Log("Amount of objects binded: " + binders.Count);
            listIndex = binders.Count;
            TriggerNextEventInQueue();
        }
    }

    public static void BindToEvent(EffectLogic_SO c, Trigger t)
    {
        bool added = false;
        Debug.Log(c + "is bound to trigger event");
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
        int i = triggerBindList.Count -1;
        bool searching = true;
        while (searching)
        {
            if (triggerBindList[i].hasTriggered)
            {

            }
            else
            {
                
            }
        }

        /*
         * FINISH THIS STUFF. ADD THE LIST IN LIST BINDING SEARCH FOR TriggerBindList and EffectLogic_SO inside it
         * 
         */
        if (listIndex >= binders.Count)
        {
            Debug.Log("All events triggered!");
            AllEventsTriggered();
        }
        else
        {
            EffectLogic_SO.eventTriggerCompleted += TriggerNextEventInQueue;
            triggerBindList[listIndex].TriggerActivation();
        }
    }

    private static void AllEventsTriggered()
    {
        EffectLogic_SO.eventTriggerCompleted -= TriggerNextEventInQueue;
        binders.Clear();
    }
}

public class TriggerBindList
{
    public Trigger type;
    public List<EffectLogic_SO> list = new();
    public bool hasTriggered = false;
}
