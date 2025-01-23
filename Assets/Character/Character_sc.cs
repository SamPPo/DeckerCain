using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Decker;
using System.Linq;

public class Character_sc : MonoBehaviour
{
    //Card piles
    private DeckPile_sc deckPile;
    public void SetDeckPile(DeckPile_sc c) { deckPile = (DeckPile_sc)c; }
    public void AddCardToDeck(Card_SO card) { deckPile.AddCard(card); }
    private PlayPile_sc playPile;
    private DiscardPile_sc discardPile;
    private SpentPile_sc spentPile;

    public Faction faction;
    public Inventory_sc inventory;

    public Transform deckT;
    public Transform displayT;
    public Transform discardT;
    
    private Card_SO cardInPlay;

    //delegates
    private delegate void WaitTimerDelegate();
    private WaitTimerDelegate waitTimerDone;

    public delegate void EndTurnDelegate();
    public static EndTurnDelegate endTurn;

    public delegate void StartTurnDelegate();
    public static StartTurnDelegate startTurn;

    public void InitializeCharacter()
    {
        GetComponent<Attributes_sc>().InitializeAttributes();
        GetComponent<HealthBar_sc>().InitializeSliders(GetComponent<Attributes_sc>().Health);
        deckPile = new();
        playPile = new();
        discardPile = new();
        spentPile = new();
    }

    public void StartCombat()
    {
        deckPile.ShufflePile();
    }

    public void StartTurn()
    {
        Debug.Log("Character_sc.START Turn");
        PlayACardChain();
    }

    private void PlayACardChain()
    {
        waitTimerDone -= PlayACardChain;
        cardInPlay = deckPile.GetTopCard();
        if (cardInPlay != null)
        {
            Card_SO.cardPlayFinished += StartAfterCardPlayWait;
            cardInPlay.PlayCard();
        }
        else
        {
            EndTurn();
        }
    }

    private void StartAfterCardPlayWait(bool end)
    {
        Card_SO.cardPlayFinished -= StartAfterCardPlayWait;

        playPile.AddCard(cardInPlay);
        cardInPlay = null;
        if (end)
        {
            waitTimerDone += EndTurn;
        }
        else
        {
            waitTimerDone += PlayACardChain;
        }
        
        StartCoroutine(WaitTimer(Pvsc.GetWaitTime(WaitTime.Medium)));
    }

    public void EndTurn()
    {
        waitTimerDone -= EndTurn;

        playPile.GetAllCards().ForEach(card => discardPile.AddCard(card));
        playPile.ClearPile();

        if (!deckPile.GetAllCards().Any())
            ShuffleDeck();
        else
            FinalizeEndTurn();
    }

    public void ShuffleDeck()
    {
        discardPile.GetAllCards().ForEach(card => deckPile.AddCard(card));
        discardPile.ClearPile();
        deckPile.ShufflePile();
        waitTimerDone += ShuffleCompleted;
        StartCoroutine(WaitTimer(Pvsc.GetWaitTime(WaitTime.Medium)));
    }

    private void ShuffleCompleted()
    {
        waitTimerDone -= ShuffleCompleted;
        TriggerHandler.allEventsTriggered += FinalizeEndTurn;
        TriggerHandler.TriggerEvent(Trigger.OnShuffleDeck, gameObject, gameObject);
    }

    private void FinalizeEndTurn()
    {
        TriggerHandler.allEventsTriggered -= FinalizeEndTurn;
        Debug.Log("Character_sc.END Turn");
        TriggerHandler.ResetAllTriggers();
        endTurn?.Invoke();
    }

    IEnumerator WaitTimer(float time)
    {
        yield return new WaitForSeconds(time);
        waitTimerDone?.Invoke();
    }
}
