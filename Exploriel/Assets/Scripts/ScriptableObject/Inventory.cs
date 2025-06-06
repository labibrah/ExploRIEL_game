using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int coins;

    public void AddItem(Item item)
    {
        if (item != null && !items.Contains(item))
        {
            items.Add(item);
            currentItem = item; // Set the current item to the newly added item
            Debug.Log("Added item: " + item.itemName);
        }
        else
        {
            Debug.LogWarning("Item is null or already exists in the inventory.");
        }
    }

    public bool hasItem(Item item)
    {
        return items.Contains(item);
    }
}
