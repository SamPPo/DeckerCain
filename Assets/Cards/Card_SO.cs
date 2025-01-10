using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Decker;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "Card_SO", menuName = "Scriptable Objects/Card_SO")]
public class Card_SO : ScriptableObject
{
    public string cardName;
    public List<EffectPayload> effectPayloads;
    public List<Keyword> keywords;
    public PositionPreference positionPreference;

    private int effectIndex;
    private List<EffectLogic_SO> effectLogics = new();
    public void AddEffectLogic(EffectLogic_SO e)
    { effectLogics.Add(e); }

    public void PlayCard()
    {
        foreach (EffectLogic_SO e in effectLogics)
        {
            Debug.Log("PLAY CARD");
            e.BindToTriggerDelegates();
        }
        //TriggerHandler.triggersDone += PlayEffects;
        TriggerHandler.EventTrigger(Trigger.OnCardPlay);
    }

    private void PlayEffects()
    {
        //TriggerHandler.triggersDone -= PlayEffects;
        effectIndex = 0;
        effectLogics[effectIndex].PlayEffect();
    }
}

