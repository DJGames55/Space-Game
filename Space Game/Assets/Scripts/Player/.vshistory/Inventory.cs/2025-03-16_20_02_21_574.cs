using System;
using System.Collections.Generic;
using UnityEngine;
using static StarLocations;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> Items = new List<Item>();

    public void AddItem(string itemName)
    {
        foreach (Item item in Items)
        {
            if (item.Name == itemName)
            {
                item.Count++;
                return;
            }
        }

        var newItem = new Item
        {
            Name = itemName,
            Count = 1,
        };

        Items.Add(newItem);
    }

    public void RemoveItem(string itemName)
    {
        foreach (Item item in Items)
        {
            if (item.Name == itemName)
            {
                Items.Remove(item);
                return;
            }
        }
    }
}

[Serializable]
public class Item
{
    public string Name;
    public int Count;
}