using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class TriggerHandler_sc
{
    public delegate void StartRoundDelegate();
    public static StartRoundDelegate startRound;


    private static List<EffectContainer_sc> binders = new();
    private static int triggerIndex;

    public static void StartRoundPrecall()
    {
        startRound?.Invoke();
        if (binders.Count > 0) { TriggerNextEvent(); }
    }

    public static void BindToEvent(EffectContainer_sc c)
    {
        binders.Add(c);
    }

    private static void TriggerNextEvent()
    {
        if (triggerIndex >= binders.Count)
        {
            AllEventsTriggered(); 
        }
        else
        {
            binders[triggerIndex].eventTriggerComplete += TriggerNextEvent;
            binders[triggerIndex].TriggerActivation();
            triggerIndex++;
        }

    }

    private static void AllEventsTriggered()
    {
        triggerIndex = 0;
        binders.Clear();
    }
}
