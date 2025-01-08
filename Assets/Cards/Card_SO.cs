using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Decker;
using System;

[CreateAssetMenu(fileName = "Card_SO", menuName = "Scriptable Objects/Card_SO")]
public class Card_SO : ScriptableObject
{
    [SerializeField]
    private Card card;
}

[Serializable]
public class Card
{
    public string cardName;
    public List<EffectPayload> effectPayloads;
    public List<Keyword> keywords;
    public PositionPreference positionPreference;
}
