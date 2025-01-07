using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TurnAllocator_sc
{
    private List<GameObject> characters;
    public void SetCharacters(List<GameObject> c) { characters = c; }

    int turnIndex = 0;

    public void StartNextRound()
    {
        turnIndex = 0;
        GiveTurnToNextCharacter();
    }

    private void GiveTurnToNextCharacter()
    {
        if (turnIndex >= characters.Count)
        {
            //StartNextRound();
        }
        else
        {
            Character_sc.endTurn += OnCharacterTurnEnd;
            characters[turnIndex].GetComponent<Character_sc>().StartTurn();
        }
    }

    private void OnCharacterTurnEnd()
    {
        Character_sc.endTurn -= OnCharacterTurnEnd;
        turnIndex++;
        GiveTurnToNextCharacter();
    }
}
