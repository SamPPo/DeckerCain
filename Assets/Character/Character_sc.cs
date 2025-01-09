using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Character_sc : MonoBehaviour
{
    //Card piles
    private DeckPile_sc deckPile;
    public void SetDeckPile(DeckPile_sc c) { deckPile = (DeckPile_sc)c; }

    private PlayPile_sc playPile;
    private SpentPile_sc spentPile;
    //private Card_sc cardBeingPlayed;

    //delegates
    public delegate void EndTurnDelegate();
    public static EndTurnDelegate endTurn;

    public delegate void StartTurnDelegate();
    public static StartTurnDelegate startTurn;


    public void StartTurn()
    {
        Debug.Log("Character_sc.START Turn");
        PlayACardChain();
        StartCoroutine(WaitForTurnEnd());
    }

    IEnumerator WaitForTurnEnd()
    {
        yield return new WaitForSeconds(Pvsc.W_AfterTurn);
        EndTurn();
    }

    public void EndTurn()
    {
        Debug.Log("Character_sc.END Turn");
        endTurn?.Invoke();
    }

    private void PlayACardChain()
    {
        var cardBeingPlayed = GetTopCardOfDeck();
        cardBeingPlayed.PlayCard();
    }

    private Card_sc GetTopCardOfDeck()
    {
        return deckPile.GetCardAtIndex(0);
    }
}
