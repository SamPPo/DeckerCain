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
        InstantiateCardPfabToTransform(CardPile.Deck);

        CardMovements_sc.movementCompleted += PlayNextEffect;
        MoveCardToPile(CardPile.Display, WaitTime.Medium);
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
        MoveCardToPile(CardPile.Discard, WaitTime.Medium);
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

    private void MoveCardToPile(CardPile cp, WaitTime wt)
    {
        DTransform trans = new();
        trans.MakeCameraFacingTransform(GetPileTransform(cp));
        cardPfab.GetComponent<CardMovements_sc>().MoveCardToTransform(trans, Pvsc.GetWaitTime(wt));
    }

    public void InstantiateCardPfabToTransform(CardPile cp)
    {
        //spawn card and set its Transform
        Transform t = GetPileTransform(cp);
        cardPfab = Instantiate(GameMaster_sc.GetCardPfab(), t.position, t.rotation);
        cardPfab.transform.localScale = t.localScale;
    }

    private Transform GetPileTransform(CardPile cp)
    {
        return cp switch
        {
            CardPile.Deck => deck,
            CardPile.Discard => discard,
            CardPile.Display => display,
            CardPile.Spent => null,
            _ => throw new ArgumentOutOfRangeException(nameof(cp), cp, null),
        };
    }
}

