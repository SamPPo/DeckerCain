using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

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
        //TriggerHandler_sc.StartRoundPrecall();
    }
}
