using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Decker;
using System;
using System.Collections;
using System.Linq;

[CreateAssetMenu(fileName = "Card_SO", menuName = "Card_SO")]
public class Card_SO : EffectContainer_SO
{
    public delegate void CardPlayFinishedDelegate();
    public static CardPlayFinishedDelegate cardPlayFinished;

    public List<Keyword> keywords;
    public PositionPreference positionPreference;

    private int effectIndex;


    public void PlayCard()
    {
        TriggerHandler.allEventsTriggered += TriggersDone;
        if (effectLogics.Any())
        {
            effectIndex = 0;
            PlayNextEffect();
        }
    }

    private void TriggersDone()
    {
        TriggerHandler.allEventsTriggered -= TriggersDone;
        PlayNextEffect();
    }

    private void PlayNextEffect()
    {
        if (effectIndex < effectLogics.Count)
        {
            effectLogics[effectIndex].ActivateEffect();
            effectIndex++;
        }
        else
        {
            AllEffectsPlayed();
        }
    }

    private void AllEffectsPlayed()
    {
        TriggerHandler.allEventsTriggered += WaitBeforeFinishingCardPlay;
        TriggerHandler.TriggerEvent(Trigger.OnCardPlay);
    }

    private void WaitBeforeFinishingCardPlay()
    {
        TriggerHandler.allEventsTriggered -= WaitBeforeFinishingCardPlay;
        Waiter_sc.waitEnded += CardPlayFinished;
        Wait.w.StartWait(Pvsc.GetWaitTime(WaitTime.Medium));
    }

    private void CardPlayFinished()
    {
        Waiter_sc.waitEnded -= CardPlayFinished;
        Debug.Log("Card_SO: Card play finished!");
        cardPlayFinished?.Invoke();
    }
}

