using UnityEngine;
using Decker;
using Unity.Mathematics;
using NUnit.Framework;
using System.Collections.Generic;

public class DeckPile_sc : CardPile_sc
{
    
    public override void AddCard(Card_SO c)
    {
        Debug.Log("DeckPile_sc: Add card to deck " + c);
        switch (c.positionPreference)
        {
            case PositionPreference.Random:
                AddCardToRandom(c);
                break;

            case PositionPreference.Top:
                AddCardToTop(c);
                break;

            case PositionPreference.Upper:
                AddCardToUpper(c);
                break;

            case PositionPreference.Lower:
                AddCardToLower(c);
                break;

            case PositionPreference.Bottom:
                AddCardToBottom(c);
                break;
        }
    }

    private void AddCardToRandom(Card_SO c)
    {
        int i = UnityEngine.Random.Range(0, cards.Count + 1);
        if (i == cards.Count)
            cards.Add(c);
        else
            cards.Insert(i, c);
    }

    private void AddCardToTop(Card_SO c)
    {
        cards.Add(c);
    }

    private void AddCardToUpper(Card_SO c)
    {
        int half = (int)math.ceil(cards.Count / 2);
        int i = UnityEngine.Random.Range(half, cards.Count + 1);
        if (i == cards.Count)
            cards.Add(c);
        else
            cards.Insert(i, c);
    }

    private void AddCardToLower(Card_SO c)
    {
        int half = (int)math.floor(cards.Count / 2);
        int i = UnityEngine.Random.Range(0, half);
        cards.Insert(i, c);
    }

    private void AddCardToBottom(Card_SO c)
    {
        cards.Insert(0, c);
    }

    public override void ShufflePile()
    {
        List<Card_SO> topCards = new();
        List<Card_SO> bottomCards = new();
        List<Card_SO> upperCards = new();
        List<Card_SO> lowerCards = new();
        List<Card_SO> randomCards = new();

        foreach (Card_SO c in cards)
        {
            switch (c.positionPreference)
            {
                case PositionPreference.Top:
                    topCards.Add(c);
                    break;
                case PositionPreference.Upper:
                    upperCards.Add(c);
                    break;
                case PositionPreference.Lower:
                    lowerCards.Add(c);
                    break;
                case PositionPreference.Bottom:
                    bottomCards.Add(c);
                    break;
                case PositionPreference.Random:
                    randomCards.Add(c);
                    break;
            }
        }
        cards.Clear();
        topCards = ShuffleRandom(topCards);
        bottomCards = ShuffleRandom(bottomCards);
        upperCards = ShuffleRandom(upperCards);
        lowerCards = ShuffleRandom(lowerCards);
        randomCards = ShuffleRandom(randomCards);

        cards = lowerCards;
        cards.AddRange(upperCards);

        foreach (Card_SO c in randomCards)
        {
            AddCardToRandom(c);
        }

        foreach (Card_SO c in bottomCards)
        {
            AddCardToBottom(c);
        }

        cards.AddRange(topCards);
    }
}
