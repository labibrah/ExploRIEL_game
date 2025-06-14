using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Supply
{
    public Inventory playerInventory;



    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            playerInventory.coins++;
            Debug.Log("Coin collected! Total coins: " + playerInventory.coins);
            supplySignal.Raise(); // Notify that the coin has been collected
            yield return new WaitForSeconds(0.3f); // Optional delay for sound effect
            Destroy(gameObject); // Destroy the coin after collection
        }
        else
        {
            Debug.LogWarning("Coin collision with non-player object: " + collision.gameObject.name);
        }
    }
}
