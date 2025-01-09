using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Decker;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "Card_SO", menuName = "Scriptable Objects/Card_SO")]
public class Card_SO : ScriptableObject
{
    public CardData cardData;

    public void PlayCard()
    {
        foreach (var effectPayload in cardData.effectPayloads)
        {
            effectPayload.PlayEffectPayload();
            Debug.Log("Played " + cardData.cardName);
        }
    }
}

