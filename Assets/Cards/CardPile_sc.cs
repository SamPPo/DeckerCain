using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CardPile_sc
{
    protected List<Card_SO> cards = new();
    public int GetPileSize() { return cards.Count; }

    public void AddCard(Card_SO c)
    {
        cards.Add(c);
    }

    public Card_SO GetCardAtIndex(int i)
    {
        if (cards.Count == 0)
        {
            return null;
        }
        else
        {
            var card = cards[i];
            cards.RemoveAt(i);
            return card;
        }
    }

    public void RemoveCardAtIndex(int i)
    {
        cards.RemoveAt(i);
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
}
