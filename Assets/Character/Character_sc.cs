using System.Collections;
using UnityEngine;

public class Character_sc : MonoBehaviour
{
    //Card piles
    private CardPile_sc deckPile;
    private CardPile_sc playPile;
    private CardPile_sc spentPile;

    //delegates
    public delegate void EndTurnDelegate();
    public static EndTurnDelegate endTurn;

    public void StartTurn()
    {
        Debug.Log("Character_sc.StartTurn");
        StartCoroutine(WaitWhileTurnEnds());
    }

    IEnumerator WaitWhileTurnEnds()
    {
        yield return new WaitForSeconds(Pvsc.W_AfterTurn);
        EndTurn();
    }

    public void EndTurn()
    {
        Debug.Log("Character_sc.EndTurn");
        endTurn?.Invoke();
    }
}
