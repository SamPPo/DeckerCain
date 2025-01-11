using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_sc
{
    private List<Item_SO> items = new();
    public void AddItemToInventory(Item_SO g)
    {
        items.Add(g);
    }
}
