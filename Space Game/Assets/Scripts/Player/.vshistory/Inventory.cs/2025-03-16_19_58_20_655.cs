using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    
}

[Serializable]
public class Item
{
    public string Name;
    public int Count;
}