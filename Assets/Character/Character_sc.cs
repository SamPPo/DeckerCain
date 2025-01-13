using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Decker;

public class Character_sc : MonoBehaviour
{
    //Card piles
    private DeckPile_sc deckPile;
    public void SetDeckPile(DeckPile_sc c) { deckPile = (DeckPile_sc)c; }

    public Faction faction;
    public Inventory_sc inventory;
    public GameObject card_pfab;

    public Transform deckT;
    public Transform displayT;
    public Transform playT;

    private PlayPile_sc playPile;
    private SpentPile_sc spentPile;
    private Card_SO cardSOInPlay;
    private GameObject cardInPlay;

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
        cardSOInPlay = GetTopCardOfDeck();
        if (cardSOInPlay != null)
        {
            MoveCardToDisplay(Pvsc.GetWaitTime(WaitTime.Medium));
        }
        else { Debug.Log("Deck EMPTY!"); }
    }

    private Card_SO GetTopCardOfDeck()
    {
        return deckPile.GetCardAtIndex(0);
    }


    private void PlayCard()
    {
        waitTimer -= PlayCard;
        Card_SO.cardPlayFinished += StartAfterCardPlayWait;
        cardSOInPlay.PlayCard();
    }

    private void StartAfterCardPlayWait()
    {
        Card_SO.cardPlayFinished -= StartAfterCardPlayWait;
        MoveCardToPlaypile(Pvsc.GetWaitTime(WaitTime.Short));

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

    private void MoveCardToDisplay(float time)
    {
        waitTimer += PlayCard;

        // Instantiate the new card and spawn it to the deck transform
        cardInPlay = Instantiate(card_pfab, deckT.transform.position, deckT.transform.rotation);
        cardInPlay.transform.localScale = deckT.transform.localScale;

        DTransform trans = new();
        trans.MakeCameraFacingTransform(displayT.transform);
        cardInPlay.GetComponent<CardMovements_sc>().MoveCardToTransform(trans, time);

        StartCoroutine(WaitTimer(time));
    }

    private void MoveCardToPlaypile(float time)
    {
        DTransform trans = new();
        trans.MakeTransform(playT.transform);
        cardInPlay.GetComponent<CardMovements_sc>().MoveCardToTransform(trans, time);
    }

    IEnumerator WaitTimer(float time)
    {
        yield return new WaitForSeconds(time);
        waitTimer?.Invoke();
    }
}
