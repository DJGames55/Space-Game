using System;
using System.Collections.Generic;
using UnityEngine;
using static StarLocations;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> distances = new List<Item>();

    public void AddItem(string itemName)
    {

    }
}

[Serializable]
public class Item
{
    public string Name;
    public int Count;
}