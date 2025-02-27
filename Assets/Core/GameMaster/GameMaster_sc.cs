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
    private List<Transform> playerSpawnPoints;
    [SerializeField]
    private List<Transform> enemySpawnPoints;
    [SerializeField]
    private PlayerMaster_sc playerMaster;
    [SerializeField]
    private EnemyMaster_sc enemyMaster;
    //[SerializeField]
    //private List<Card_SO> debugDeck;
    [SerializeField]
    private List<Item_SO> debugItems;
    [SerializeField]
    private GameObject cardTemplate;
    private static GameObject cardPfab;
    

    private void Start()
    {
        InitializeGameMaster();

        //Initialize character masters and instantiate characters
        characters.AddRange(playerMaster.InstantiateCharacters(playerSpawnPoints));
        characters.AddRange(enemyMaster.InstantiateCharacters(enemySpawnPoints));

        //Pass initialized characters to turn allocator
        TurnAllocator_sc.SetCharacters(characters);
    }

    private void InitializeGameMaster()
    {
        cardPfab = cardTemplate;
    }

    //Start combat -> TurnAllocator_sc handles the turns
    [ContextMenu("StartCombat")]
    void StartCombat()
    {
        TurnAllocator_sc.StartCombat();
    }


    //!!!!!PURELY FOR DEBUG PROTO PURPOSES!!!!!

    //Add items to character and initialize script variables
    private void DebugInitializeCharacter(GameObject c)
    {
        DEBUGAddItemsToCharacter(c);
        c.GetComponent<Character_sc>().InitializeCharacter();
    }

    private void DEBUGAddItemsToCharacter(GameObject c)
    {
        c.GetComponent<Character_sc>().inventory = new();
        foreach (var item in debugItems)
        {
            var newItem = InstantiateAndInitializePresetItem(item, c);
            newItem.BindEffectsToTriggers();
            c.GetComponent<Character_sc>().inventory.AddItemToInventory(newItem);
        }
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
        //Instantiate Card_SO and set targetCharacter as owner
        var newCard = Instantiate(presetCard);
        newCard.SetOwner(targetCharacter);

        //Instantiate effects and bind them to the card
        foreach (var ep in presetCard.effectPayloads)
        {
            var newEf = Instantiate(ep.effect);

            //Initialize effect with the effect payload data
            newEf.InitializeEffect(ep.MakeEffectData(targetCharacter, newCard));
            newCard.AddEffectLogic(newEf);
        }

        var t = targetCharacter.GetComponent<Character_sc>();
        newCard.effectPayloads.Clear();
        return newCard;
    }

    public static Item_SO InstantiateAndInitializePresetItem(Item_SO presetItem, GameObject targetCharacter)
    {
        //Instantiate Item_SO and set targetCharacter as owner
        var newItem = Instantiate(presetItem);
        newItem.SetOwner(targetCharacter);

        //Instantiate effects and bind them to the item
        foreach (var ep in presetItem.effectPayloads)
        {
            var newEf = Instantiate(ep.effect);

            //Initialize effect with the effect payload data
            newEf.InitializeEffect(ep.MakeEffectData(targetCharacter, newItem));
            newItem.AddEffectLogic(newEf);
        }
        newItem.effectPayloads.Clear();

        //Bind effects to triggers
        newItem.BindEffectsToTriggers();
        return newItem;
    }
}
