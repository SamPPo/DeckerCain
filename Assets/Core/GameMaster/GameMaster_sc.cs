using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Decker;
using System.Collections;
using System.Linq;

public class GameMaster_sc : MonoBehaviour
{
    private List<GameObject> characters = new();
    public List<GameObject> GetCharacters() { return characters; }

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
    private GameObject cardPfab;
    public GameObject GetCardPfab() { return cardPfab; }

    private void Start()
    {
        GameMaster.SetGameMaster(this); //Set game master to the static class

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
            DEBUGAddDeckToCharacter(c.GetComponent<Character_sc>());
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
                newEf.SetEffectData(ep.MakeEffectData(c.gameObject));
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
                newEf.SetEffectData(ep.MakeEffectData(c.gameObject));
                newItem.AddEffectLogic(newEf);
            }
            newItem.effectPayloads.Clear();
            newItem.BindEffectsToTriggers();
            c.inventory.AddItemToInventory(newItem);
        }
    }
}

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
