using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory; // Reference to the player's inventory
    public TextMeshProUGUI coinText; // Reference to the UI Text component for displaying coins


    void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player Inventory is not assigned in CoinTextManager.");
            return;
        }

        if (coinText == null)
        {
            Debug.LogError("Coin Text UI component is not assigned in CoinTextManager.");
            return;
        }

        UpdateCoinText(); // Initialize the coin text at the start
    }

    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "" + playerInventory.coins; // Update the text with the current coin count
        }
        else
        {
            Debug.LogWarning("Coin Text UI component is not assigned.");
        }
    }
}
