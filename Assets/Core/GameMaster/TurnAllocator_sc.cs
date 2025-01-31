using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Decker;
using System.Linq;

public static class TurnAllocator_sc
{
    private static List<GameObject> characters;
    private static List<GameObject> enemyCharacters;
    private static List<GameObject> playerCharacters;
    public static void SetCharacters(List<GameObject> c)
    {
        characters = c;
        enemyCharacters = characters.Where(c => c.GetComponent<Character_sc>().faction == Faction.Enemy).ToList();
        playerCharacters = characters.Where(c => c.GetComponent<Character_sc>().faction == Faction.Player).ToList();
    }

    private static List<CharacterMaster_sc> characterMasters;
    public static void SetCharacterMasters(List<CharacterMaster_sc> cm) { characterMasters = cm; }


    private static int turnIndex = 0;
    private static int playerTurnIndex = 0;
    private static int enemyTurnIndex = 0;
    private static bool isPlayerTurn = true;


    //Start combat
    public static void StartCombat()
    {
        foreach (var c in characters)
        {
            c.GetComponent<Character_sc>().InitializeCharacter();
        }

        ReordedCharactersBasedOnInitiative();
        TriggerHandler.allEventsTriggered += StartCombatRound;
        TriggerHandler.TriggerEvent(Trigger.OnCombatStart);
    }

    //Reorder characters based on initiative
    private static void ReordedCharactersBasedOnInitiative()
    {
        enemyCharacters = enemyCharacters
            .OrderBy(c => c.GetComponent<Character_sc>().GetInitiative())
            .ToList();

        playerCharacters = playerCharacters
            .OrderBy(c => c.GetComponent<Character_sc>().GetInitiative())
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
        if (isPlayerTurn)
            GiveTurnToNextPlayerCharacter();
        else
            GiveTurnToNextEnemyCharacter();
    }

    private static void GiveTurnToNextPlayerCharacter()
    {
        var pC = playerCharacters[playerTurnIndex].GetComponent<Character_sc>();
        playerTurnIndex++;
        if (playerTurnIndex < playerCharacters.Count)
            playerTurnIndex = 0;
        isPlayerTurn = false;
        pC.StartTurn();
    }

    private static void GiveTurnToNextEnemyCharacter()
    {
        var eC = enemyCharacters[enemyTurnIndex].GetComponent<Character_sc>();
        enemyTurnIndex++;
        if (enemyTurnIndex < enemyCharacters.Count)
            enemyTurnIndex = 0;
        isPlayerTurn = true;
        eC.StartTurn();
    }



    private static void OnCharacterTurnEnd()
    {
        Character_sc.endTurn -= OnCharacterTurnEnd;
        turnIndex++;
        GiveTurnToNextCharacter();
    }
}
