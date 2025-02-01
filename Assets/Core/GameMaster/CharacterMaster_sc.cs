using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMaster_sc : MonoBehaviour
{
    [SerializeField]
    public List<Character_SO> debugCharacters;

    public List<GameObject> InstantiateCharacters(List<Transform> spawnPoints)
    {
        int spawnIndex = 0;
        List<GameObject> characters = new();

        foreach (Character_SO cSO in debugCharacters)
        {
            if (spawnIndex >= spawnPoints.Count)
            {
                Debug.LogError("Not enough spawn points for all characters!");
                return characters;
            }

            GameObject c = Instantiate(cSO.characterPfab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
            var newCharacter = c.GetComponent<Character_sc>();
            newCharacter.faction = cSO.faction;

            //Initialize character and all the classes required to run it
            newCharacter.InitializeCharacter();

            foreach (Card_SO card in cSO.presetDeck)
            {
                var newCard = GameMaster_sc.InstantiateAndInitializePresetCard(card, c);
                newCharacter.AddCardToDeck(newCard);
            }
            foreach (Item_SO item in cSO.presetItems)
            {
                var newItem = GameMaster_sc.InstantiateAndInitializePresetItem(item, c);
                newCharacter.inventory.AddItemToInventory(newItem);
            }
            characters.Add(c);
            spawnIndex++;
        }

        return characters;
    }
}
