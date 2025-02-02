using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Decker;
using System.Linq;
using Unity.Mathematics.Geometry;

public static class TurnAllocator_sc
{
    private static List<GameObject> characters;
    private static List<GameObject> turnOrderCharacters;
    public static void SetCharacters(List<GameObject> c)
    { characters = c; }

    private static int turnIndex = 0;

    //Start combat
    public static void StartCombat()
    {
        ReordedCharactersBasedOnInitiative();
        TriggerHandler.allEventsTriggered += StartCombatRound;
        TriggerHandler.TriggerEvent(Trigger.OnCombatStart);
    }

    //Reorder characters based on initiative
    private static void ReordedCharactersBasedOnInitiative()
    {
        turnOrderCharacters = characters.OrderBy(c => c.transform.position.x)
                                        .ThenBy(c => Mathf.Abs(c.transform.position.z))
                                        .ThenBy(c => c.GetComponent<Character_sc>().faction == Faction.Player ? 0 : 1)
                                        .ToList();
    }

    //Give turn to next character
    public static void StartCombatRound()
    {
        TriggerHandler.allEventsTriggered -= StartCombatRound;
        turnIndex = 0;
        GiveTurnToNextCharacter();
    }

    private static void GiveTurnToNextCharacter()
    {
        Character_sc.endTurn += OnCharacterTurnEnd;
        turnOrderCharacters[turnIndex].GetComponent<Character_sc>().StartTurn();
    }

    private static void OnCharacterTurnEnd()
    {
        Character_sc.endTurn -= OnCharacterTurnEnd;
        turnIndex++;
        if (turnIndex >= turnOrderCharacters.Count)
            turnIndex = 0;
        GiveTurnToNextCharacter();
    }

    public static void RemoveCharacterFromTurnOrder(GameObject c)
    {
        turnOrderCharacters.Remove(c);
        characters.Remove(c);
    }
}
