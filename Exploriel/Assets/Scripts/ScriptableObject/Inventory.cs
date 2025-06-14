using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int coins;

    public void OnBeforeSerialize()
    {
        // This method is called before serialization, you can add any pre-serialization logic here if needed.
    }

    public void OnAfterDeserialize()
    {
        // This method is called after deserialization, you can add any post-deserialization logic here if needed.
        // Clear inventory on game start
        items.Clear();
        currentItem = null;
        coins = 0;
    }
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
