using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "ScriptableObjects/Inventory")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int coins;

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    private static void RegisterInventoryResetOnPlayMode()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            string[] guids = AssetDatabase.FindAssets("t:Inventory");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Inventory asset = AssetDatabase.LoadAssetAtPath<Inventory>(path);
                if (asset != null)
                {
                    asset.ResetInventory();
                    EditorUtility.SetDirty(asset); // Update in Inspector if needed
                }
            }
        }
    }
#endif

    public void ResetInventory()
    {
        items.Clear();
        currentItem = null;
        coins = 0;
    }

    public void AddItem(Item item)
    {
        if (item != null)
        {
            items.Add(item);
            currentItem = item;
            Debug.Log("Added item: " + item.itemName);
        }
        else
        {
            Debug.LogWarning("Item is null or already exists in the inventory.");
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize()
    {
        // No need to reset here — we now reset only when entering Play Mode
    }

    public int GetItemCount(Item item)
    {
        int count = 0;
        foreach (var i in items)
        {
            if (i == item)
            {
                count++;
            }
        }
        return count;
    }
}
