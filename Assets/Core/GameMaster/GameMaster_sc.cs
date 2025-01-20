using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Decker;
using System.Collections;
using System.Linq;

public class GameMaster_sc : MonoBehaviour
{
    private static List<GameObject> characters = new();
    //public List<GameObject> GetCharacters() { return characters; }

    //FOR DEBUG ONLY!
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private List<GameObject> spawnCharacters;
    [SerializeField]
    private List<Card_SO> debugDeck;
    [SerializeField]
    private List<Item_SO> debugItems;
    [SerializeField]
    private GameObject cardTemplate;
    private static GameObject cardPfab;
    

    private void Start()
    {
        cardPfab = cardTemplate;
        int i = 0;
        foreach (Transform p in spawnPoints)
        {
            //!!!!!!!FOR DEBUG ONLY!!!!!!!
            var c = Instantiate(spawnCharacters[i], p);
            characters.Add(c);
            if (i==0)
                c.GetComponent<Character_sc>().faction = Faction.Player;
            else
                c.GetComponent<Character_sc>().faction = Faction.Enemy;
            DEBUGAddDeckToCharacter(c);
            c.GetComponent<Character_sc>().InitializeCharacter();
            i++;
        }
        InitializeScripts();
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

    /*
     * STATIC METHODS FOR GENERIC STUFF!
    */

    public static GameObject GetCardPfab()
    { 
        return cardPfab;
    }

    public static List<GameObject> GetCharacters()
    {
        if (characters.Any())
            return characters;
        else
            return null;
    }

    public static Card_SO InstantiateAndInitializePresetCard(Card_SO presetCard, GameObject targetCharacter)
    {
        var newCard = Instantiate(presetCard);
        foreach (var ep in presetCard.effectPayloads)
        {
            var newEf = Instantiate(ep.effect);
            newEf.SetEffectData(ep.MakeEffectData(targetCharacter));
            newCard.AddEffectLogic(newEf);
            var t = targetCharacter.GetComponent<Character_sc>();
            newCard.SetPileTransforms(t.deckT, t.discardT, t.displayT);
        }
        newCard.effectPayloads.Clear();
        return newCard;
    }

    public static Item_SO InstantiateAndInitializePresetItem(Item_SO presetItem, GameObject targetCharacter)
    {
        var newItem = Instantiate(presetItem);
        foreach (var ep in presetItem.effectPayloads)
        {
            var newEf = Instantiate(ep.effect);
            newEf.SetEffectData(ep.MakeEffectData(targetCharacter));
            newItem.AddEffectLogic(newEf);
        }
        newItem.effectPayloads.Clear();
        return newItem;
    }

    //!!!!!PURELY FOR DEBUG PROTO PURPOSES!!!!!
    private void DEBUGAddDeckToCharacter(GameObject c)
    {
        DeckPile_sc deck = new();
        foreach (var cardSO in debugDeck)
        {
            var newCard = InstantiateAndInitializePresetCard(cardSO, c);
            deck.AddCard(newCard);
        }
        c.GetComponent<Character_sc>().SetDeckPile(deck);

        c.GetComponent<Character_sc>().inventory = new();
        foreach (var item in debugItems)
        {
            var newItem = InstantiateAndInitializePresetItem(item, c);
            newItem.BindEffectsToTriggers();
            c.GetComponent<Character_sc>().inventory.AddItemToInventory(newItem);
        }
    }
}

// STATIC GAME MASTER CURRENTLY DEPRECATED
/*
public static class GameMaster
{
    private static GameMaster_sc gm;
    public static void SetGameMaster(GameMaster_sc g) { gm = g; }

    
    public static List<GameObject> GetCharacters()
    {
        if (gm.GetCharacters().Any())
            return gm.GetCharacters();
        else
            return null;
    }

    public static GameObject GetCardPfab()
    {
        return gm.GetCardPfab();
    }
    
}
*/
