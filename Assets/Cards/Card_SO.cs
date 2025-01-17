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

    private GameObject cardPfab;
    private int effectIndex;

    private Transform deck;
    private Transform discard;
    private Transform display;

    public void PlayCard()
    {
        effectIndex = 0;
        InstantiateCard();

        CardMovements_sc.movementCompleted += PlayNextEffect;
        MoveCardToDisplay();
    }

    private void TriggersDone()
    {
        TriggerHandler.allEventsTriggered -= TriggersDone;
        PlayNextEffect();
    }

    private void PlayNextEffect()
    {
        CardMovements_sc.movementCompleted -= PlayNextEffect;

        if (effectIndex < effectLogics.Count)
        {
            TriggerHandler.allEventsTriggered += TriggersDone;
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
        Debug.Log("Card_SO: All effects played!");
        CardMovements_sc.movementCompleted += WaitBeforeFinishingCardPlay;
        MoveCardToDiscard();
    }

    private void WaitBeforeFinishingCardPlay()
    {
        CardMovements_sc.movementCompleted -= WaitBeforeFinishingCardPlay;
        TriggerHandler.allEventsTriggered += CardPlayFinished;
        TriggerHandler.TriggerEvent(Trigger.OnCardPlay);
    }

    private void CardPlayFinished()
    {
        TriggerHandler.allEventsTriggered -= CardPlayFinished;
        Debug.Log("Card_SO: Card play finished!");
        cardPlayFinished?.Invoke();
    }

    public void SetPileTransforms(Transform deckT, Transform discardT, Transform displayT)
    {
        deck = deckT;
        discard = discardT;
        display = displayT;
    }

    private void MoveCardToDisplay()
    {
        DTransform trans = new();
        trans.MakeCameraFacingTransform(display);
        cardPfab.GetComponent<CardMovements_sc>().MoveCardToTransform(trans, Pvsc.GetWaitTime(WaitTime.Medium));
    }

    private void MoveCardToDiscard()
    {
        DTransform trans = new();
        trans.MakeCameraFacingTransform(discard);
        cardPfab.GetComponent<CardMovements_sc>().MoveCardToTransform(trans, Pvsc.GetWaitTime(WaitTime.Medium));
    }

    public void InstantiateCard()
    {
        //spawn card and set its Transform
        cardPfab = Instantiate(GameMaster.GetCardPfab(), deck.position, deck.rotation);
        cardPfab.transform.localScale = deck.localScale;
    }
}

