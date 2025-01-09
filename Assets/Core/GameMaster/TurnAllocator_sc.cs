using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class TurnAllocator_sc
{
    private static List<GameObject> characters;
    public static void SetCharacters(List<GameObject> c) { characters = c; }

    private static int turnIndex = 0;

    public static void StartNextRound()
    {
        turnIndex = 0;
        GiveTurnToNextCharacter();
    }

    private static void GiveTurnToNextCharacter()
    {
        if (turnIndex >= characters.Count)
        {
            StartNextRound();
        }
        else
        {
            Character_sc.endTurn += OnCharacterTurnEnd;
            characters[turnIndex].GetComponent<Character_sc>().StartTurn();
        }
    }

    private static void OnCharacterTurnEnd()
    {
        Character_sc.endTurn -= OnCharacterTurnEnd;
        turnIndex++;
        GiveTurnToNextCharacter();
    }
}
