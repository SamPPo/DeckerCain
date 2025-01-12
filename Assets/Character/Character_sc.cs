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
    public GameObject card_pfab;

    public Transform deckT;
    public Transform displayT;

    private PlayPile_sc playPile;
    private SpentPile_sc spentPile;
    private Card_SO cardBeingPlayed;

    //delegates
    private delegate void WaitTimerDelegate();
    private WaitTimerDelegate waitTimer;

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

    private void PlayACardChain()
    {
        cardBeingPlayed = GetTopCardOfDeck();
        if (cardBeingPlayed != null)
        {
            MoveCardToDisplay(Pvsc.GetWaitTime(WaitTime.Short));
        }
        else { Debug.Log("Deck EMPTY!"); }
    }

    private Card_SO GetTopCardOfDeck()
    {
        return deckPile.GetCardAtIndex(0);
    }

    private void MoveCardToDisplay(float time)
    {
        waitTimer += PlayCard;
        var card = Instantiate(card_pfab, deckT.transform.position, deckT.transform.rotation);
        card.transform.localScale = deckT.transform.localScale;
        DTransform trans = new()
        {
            position = displayT.position,
            rotation = displayT.rotation,
            scale = displayT.localScale
        };
        card.GetComponent<Card_sc>().MoveCardToTransform(trans, time);
        StartCoroutine(WaitTimer(time));
    }

    private void PlayCard()
    {
        waitTimer -= PlayCard;
        cardBeingPlayed.PlayCard();
    }

    private void StartAfterTurnWait()
    {
        Card_SO.cardPlayFinished -= StartAfterTurnWait;
        waitTimer += EndTurn;
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

    public GameObject GetTarget(Targetting t)
    {

        return null;
    }
}
