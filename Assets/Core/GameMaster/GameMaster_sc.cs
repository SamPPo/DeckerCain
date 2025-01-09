using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Decker;

public class GameMaster_sc : MonoBehaviour
{
    private List<GameObject> characters = new();

    //FOR DEBUG ONLY!
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private List<GameObject> spawnCharacters;
    [SerializeField]
    private List<Card_SO> debugDeck;

    private void Start()
    {
        int i = 0;
        foreach (Transform p in spawnPoints)
        {
            var c = Instantiate(spawnCharacters[i], p.position, p.rotation);
            characters.Add(c);
            DEBUGAddDeckToCharacter(c.GetComponent<Character_sc>());
            i++;
        }
        InitializeScripts();
    }

    //!!!!!PURELY FOR DEBUG PROTO PURPOSES!!!!!
    private void DEBUGAddDeckToCharacter(Character_sc c)
    {
        DeckPile_sc deck = new();

        foreach (var cardSO in debugDeck)
        {
            Card_sc cardComponent = c.gameObject.AddComponent<Card_sc>();
            cardComponent.SetEffectData(cardSO.cardData);
            deck.AddCard(cardComponent);
            c.SetDeckPile(deck);
        }
    }

    private void InitializeScripts()
    {
        TurnAllocator_sc.SetCharacters(characters);
    }

    /*
     * Round handling logic
    */
    [ContextMenu("StartCombat")]
    void StartCombat()
    {
        TurnAllocator_sc.StartNextRound();
        //TriggerHandler_sc.StartRoundPrecall();
    }
}
