using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Character_sc : MonoBehaviour
{
    //Card piles
    private DeckPile_sc deckPile;
    private PlayPile_sc playPile;
    private SpentPile_sc spentPile;
    private Card_sc cardBeingPlayed;

    //delegates
    public delegate void EndTurnDelegate();
    public static EndTurnDelegate endTurn;

    public delegate void StartTurnDelegate();
    public static StartTurnDelegate startTurn;



    private void Start()
    {

    }

    public void StartTurn()
    {
        Debug.Log("Character_sc.START Turn");
        StartCoroutine(WaitForTurnEnd());
    }

    IEnumerator WaitForTurnEnd()
    {
        yield return new WaitForSeconds(Pvsc.W_AfterTurn);
    }

    public void EndTurn()
    {
        Debug.Log("Character_sc.END Turn");
        endTurn?.Invoke();
    }

    private void DrawTopCardOfDeck()
    {
        cardBeingPlayed = deckPile.GetCardAtIndex(0);
    }
}
