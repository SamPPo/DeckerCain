using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Decker;

public class Character_sc : MonoBehaviour
{
    //Card piles
    private DeckPile_sc deckPile;
    public void SetDeckPile(DeckPile_sc c) { deckPile = (DeckPile_sc)c; }
    public void AddCardToDeck(Card_SO card) { deckPile.AddCard(card); }

    public Faction faction;
    public Inventory_sc inventory;
    public GameObject card_pfab;

    public Transform deckT;
    public Transform displayT;
    public Transform discardT;

    private PlayPile_sc playPile;
    private SpentPile_sc spentPile;
    private Card_SO cardInPlay;

    //delegates
    private delegate void WaitTimerDelegate();
    private WaitTimerDelegate waitTimer;

    public delegate void EndTurnDelegate();
    public static EndTurnDelegate endTurn;

    public delegate void StartTurnDelegate();
    public static StartTurnDelegate startTurn;

    public void InitializeCharacter()
    {
        GetComponent<Attributes_sc>().InitializeAttributes();
        GetComponent<HealthBar_sc>().InitializeSliders(GetComponent<Attributes_sc>().Health);
    }

    public void StartTurn()
    {
        Debug.Log("Character_sc.START Turn");
        PlayACardChain();
    }

    private void PlayACardChain()
    {
        waitTimer -= PlayACardChain;
        cardInPlay = GetTopCardOfDeck();
        if (cardInPlay != null)
        {
            Card_SO.cardPlayFinished += StartAfterCardPlayWait;
            cardInPlay.PlayCard();
        }
        else { Debug.Log("Deck EMPTY!"); }
    }

    private Card_SO GetTopCardOfDeck()
    {
        return deckPile.GetCardAtIndex(0);
    }

    private void StartAfterCardPlayWait(bool end)
    {
        Card_SO.cardPlayFinished -= StartAfterCardPlayWait;
        if (end)
        {
            waitTimer += EndTurn;
        }
        else
        {
            waitTimer += PlayACardChain;
        }
        
        StartCoroutine(WaitTimer(Pvsc.GetWaitTime(WaitTime.Medium)));
    }

    public void EndTurn()
    {
        waitTimer -= EndTurn;
        Debug.Log("Character_sc.END Turn");
        TriggerHandler.ResetAllTriggers();
        endTurn?.Invoke();
    }

    IEnumerator WaitTimer(float time)
    {
        yield return new WaitForSeconds(time);
        waitTimer?.Invoke();
    }
}
