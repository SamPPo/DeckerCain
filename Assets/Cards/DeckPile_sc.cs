using UnityEngine;
using Decker;

public class DeckPile_sc : CardPile_sc
{
    
    public override void AddCard(Card_SO c)
    {
        cards.Add(c);
    }
    
    public new void Shuffle()
    {
        
    }
}
