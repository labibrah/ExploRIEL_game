using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackForce = 200f; // Force applied to the object when knocked back
    public float knockbackDuration = 0.3f; // Duration of the knockback effect
    public float damage = 1f; // Damage dealt to the object when knocked back
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable") && this.CompareTag("Player"))
        {
            Pot pot = other.GetComponent<Pot>();
            if (pot != null)
            {
                pot.Smash(); // Call the Smash method on the Pot script
            }
            return; // Exit the method after handling the breakable object
        }
        else if (other.CompareTag("Enemy") && this.CompareTag("Player") && other.isTrigger)
        {
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized; // Calculate the direction of knockback
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce); // Apply the knockback force
                enemyRigidbody.GetComponent<Enemy>().Knock(enemyRigidbody, knockbackDuration, damage); // Call the Knock method on the Enemy script
            }
        }
        else if (other.CompareTag("Player") && this.CompareTag("Enemy") && other.isTrigger)
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                if (playerRigidbody.GetComponent<PlayerMovement>().currentState == PlayerState.stagger)
                {
                    return; // If the player is already staggered, do not apply knockback again
                }
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized; // Calculate the direction of knockback
                playerRigidbody.AddForce(knockbackDirection * knockbackForce); // Apply the knockback force
                playerRigidbody.GetComponent<PlayerMovement>().Knock(knockbackDuration, damage); // Call the Knockback method on the PlayerMovement script
            }
        }
    }


}
