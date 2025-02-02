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
    public delegate void CardPlayFinishedDelegate(bool end);
    public static CardPlayFinishedDelegate cardPlayFinished;

    public List<Keyword> keywords;
    public PositionPreference positionPreference;

    private int effectIndex;

    public void PlayCard()
    {
        effectIndex = 0;
        InstantiateCardPfabToTransform(GetPileTransform(CardPile.Deck));

        CardMovements_sc.movementCompleted += PlayNextEffect;
        MoveCardToPile(CardPile.Display, WaitTime.Short);
    }

    private void EffectActivated()
    {
        TriggerHandler.allEventsTriggered -= EffectActivated;
        PlayNextEffect();
    }

    private void PlayNextEffect()
    {
        CardMovements_sc.movementCompleted -= PlayNextEffect;

        if (effectIndex < effectLogics.Count)
        {
            TriggerHandler.allEventsTriggered += EffectActivated;
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
        Debug.Log("Card_SO, " + displayName + ": all effects played!");
        CardMovements_sc.movementCompleted += WaitBeforeFinishingCardPlay;
        MoveCardToPile(CardPile.Discard, WaitTime.Short);
    }

    private void WaitBeforeFinishingCardPlay()
    {
        CardMovements_sc.movementCompleted -= WaitBeforeFinishingCardPlay;
        TriggerHandler.allEventsTriggered += CardPlayFinished;
        TriggerHandler.TriggerEvent(Trigger.OnCardPlay, null, null);
    }

    private void CardPlayFinished()
    {
        TriggerHandler.allEventsTriggered -= CardPlayFinished;
        Debug.Log("Card_SO: Card play finished!");

        //Invoke end of turn if card has end keyword
        cardPlayFinished?.Invoke(keywords.Contains(Keyword.End));
    }

    public void MoveCardToPile(CardPile cp, WaitTime wt, float delay = 0f)
    {
        Debug.Log("Card_SO: Moving " + displayName + " to " + cp + " pile");
        bool destroyAfterMove = false;
        DTransform trans = new();
        if (cp == CardPile.Display)
        {
            trans.MakeCameraFacingTransform(GetPileTransform(cp));
        }
        else
        {
            trans.MakeTransform(GetPileTransform(cp));
            destroyAfterMove = true;
        }
        containerPfab.GetComponent<CardMovements_sc>().MoveCardToTransform(trans, Pvsc.GetWaitTime(wt), delay, destroyAfterMove);
    }

    public void InstantiateCardPfabToTransform(Transform t, bool faceCamera = false, Vector3 offset = default)
    {
        //spawn card and set its Transform
        Quaternion rot;

        if (faceCamera)
        rot = Quaternion.LookRotation(Camera.main.transform.up, -Camera.main.transform.forward);
        else
        rot = t.rotation;

        containerPfab = Instantiate(GameMaster_sc.GetCardPfab(), t.position + offset, rot);
        containerPfab.GetComponent<CardUI_sc>().SetCardText(effectLogics, keywords);
        containerPfab.GetComponent<CardUI_sc>().SetCardName(displayName);
        containerPfab.transform.localScale = t.localScale;
    }

    private Transform GetPileTransform(CardPile cp)
    {
        return cp switch
        {
            CardPile.Deck => owner.GetComponent<Character_sc>().deckT,
            CardPile.Discard => owner.GetComponent<Character_sc>().discardT,
            CardPile.Display => owner.GetComponent<Character_sc>().displayT,
            CardPile.Spent => null, //TODO: Implement spent pile
            _ => containerPfab != null ? containerPfab.transform : throw new InvalidOperationException("cardPfab is null"),
        };
    }
}

