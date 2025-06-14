using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Supply
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound); // Play the collection sound
            }
            playerHealth.runtimeValue += healthAmount; // Restore health
            playerHealth.runtimeValue = Mathf.Clamp(playerHealth.runtimeValue, 0, playerHealth.maxValue); // Ensure health does not exceed max value
            supplySignal.Raise(); // Notify that the supply has been collected
            yield return new WaitForSeconds(0.3f); // Optional delay for sound effect
            Destroy(gameObject); // Destroy the heart object after collection
        }
    }
}
