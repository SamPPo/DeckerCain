using Decker;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_SO", menuName = "Character_SO")]
public class Character_SO : ScriptableObject
{
    public GameObject characterPfab;
    public Faction faction;
    public List<Card_SO> presetDeck;
    public List<Item_SO> presetItems;

    public int startingHealth;
}
