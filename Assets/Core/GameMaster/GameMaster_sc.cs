using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Decker;
using System.Collections;

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
    [SerializeField]
    private List<Item_SO> debugItems;

    private void Start()
    {
        int i = 0;
        foreach (Transform p in spawnPoints)
        {
            var c = Instantiate(spawnCharacters[i], p);
            characters.Add(c);
            DEBUGAddDeckToCharacter(c.GetComponent<Character_sc>()); //FOR DEBUG ONLY!!!!
            i++;
        }
        InitializeScripts();
        //Wait.w.StartWait(1);
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
    }


    //!!!!!PURELY FOR DEBUG PROTO PURPOSES!!!!!
    private void DEBUGAddDeckToCharacter(Character_sc c)
    {
        DeckPile_sc deck = new();
        foreach (var cardSO in debugDeck)
        {
            var newCard = Instantiate(cardSO);
            foreach (var ep in cardSO.effectPayloads)
            {
                var newEf = Instantiate(ep.effect);
                newEf.SetEffectData(ep.MakeEffectData());
                newCard.AddEffectLogic(newEf);
            }
            newCard.effectPayloads.Clear();
            deck.AddCard(newCard);
        }
        c.SetDeckPile(deck);

        c.inventory = new();
        foreach (var item in debugItems)
        {
            var newItem = Instantiate(item);
            foreach (var ep in item.effectPayloads)
            {
                var newEf = Instantiate(ep.effect);
                newEf.SetEffectData(ep.MakeEffectData());
                newItem.AddEffectLogic(newEf);
            }
            newItem.effectPayloads.Clear();
            newItem.BindEffectsToTriggers();
            c.inventory.AddItemToInventory(newItem);
        }
    }
}
