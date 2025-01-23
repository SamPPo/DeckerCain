using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPile_sc
{
    protected List<Card_SO> cards = new();
    public int GetPileSize() { return cards.Count; }

    public virtual void AddCard(Card_SO c)
    {
        cards.Add(c);
    }

    public Card_SO GetTopCard()
    {
        if (cards.Any())
        {
            var card = cards[^1];
            cards.RemoveAt(cards.Count - 1);
            return card;
        }
        else
            return null;
    }

    public List<Card_SO> GetAllCards()
    {
        return cards;
    }

    public void ClearPile()
    {
        cards.Clear();
    }

    public Card_SO GetCardAtIndex(int i)
    {
        if (i < cards.Count)
        {
            var card = cards[i];
            cards.RemoveAt(i);
            return card;
        }
        else
        {
            Debug.Log("CardPile_sc: GetCardAtIndex(), Index out of range!");
            return null;
        }
    }

    public void RemoveCardAtIndex(int i)
    {
        cards.RemoveAt(i);
    }

    public virtual void ShufflePile()
    {
        ShuffleRandom(cards);
    }

    protected List<Card_SO> ShuffleRandom(List<Card_SO> pile)
    {
        for (int i = 0; i < pile.Count; i++)
        {
            var temp = pile[i];
            int randomIndex = Random.Range(i, pile.Count);
            pile[i] = pile[randomIndex];
            pile[randomIndex] = temp;
        }
        return pile;
    }
}
