using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CardPile_sc : MonoBehaviour
{
    private List<Card_sc> cards = new();

    public void AddCard(Card_sc c)
    {
        cards.Add(c);
    }

    public Card_sc GetCardAtIndex(int i)
    {
        var card = cards[i];
        cards.RemoveAt(i);
        return card;
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
