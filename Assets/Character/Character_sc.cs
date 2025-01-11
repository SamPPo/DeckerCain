using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Decker;

public class Character_sc : MonoBehaviour
{
    //Card piles
    private DeckPile_sc deckPile;
    public void SetDeckPile(DeckPile_sc c) { deckPile = (DeckPile_sc)c; }

    public Inventory_sc inventory;

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
        Card_SO.cardPlayFinished += StartAfterTurnWait;
        PlayACardChain();
    }

    private void StartAfterTurnWait()
    {
        Card_SO.cardPlayFinished -= StartAfterTurnWait;
        StartCoroutine(WaitForTurnEnd());
    }

    IEnumerator WaitForTurnEnd()
    {
        yield return new WaitForSeconds(Pvsc.GetWaitTime(WaitTime.Medium));
        EndTurn();
    }

    public void EndTurn()
    {
        Debug.Log("Character_sc.END Turn");
        TriggerHandler.ResetAllTriggers();
        endTurn?.Invoke();
    }

    private void PlayACardChain()
    {
        var cardBeingPlayed = GetTopCardOfDeck();
        if (cardBeingPlayed != null)
        {
            cardBeingPlayed.PlayCard();
        }
        else { Debug.Log("Deck EMPTY!"); }
            

    }

    private Card_SO GetTopCardOfDeck()
    {
        return deckPile.GetCardAtIndex(0);
    }

    //DEBUG THING

}
