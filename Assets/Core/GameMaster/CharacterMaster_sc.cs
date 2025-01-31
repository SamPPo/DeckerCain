using System.Collections.Generic;
using UnityEngine;

public class CharacterMaster_sc : MonoBehaviour
{
    [SerializeField]
    public List<Character_SO> debugCharacters;

    public void InstantiateCharacters(List<Transform> spawnPoints)
    {
        int spawnIndex = 0;
        foreach (Character_SO character in debugCharacters)
        {
            GameObject c = Instantiate(character.characterPfab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
            c.GetComponent<Character_sc>().InitializeCharacter();
        }
    }
}
