using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class FightingEnemy : MonoBehaviour
{
    public FloatValue maxHealth;
    private float health;
    public string enemyName;
    public int baseAttack;
    public float Speed;
    public GameObject deathEffectPrefab;
    public Rigidbody2D rb;
    public BoolValue isDead;

    public Animator Animation;
    public Slider healthBar; // Reference to the health bar UI element
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
        if (Animation != null)
        {
            Animation.SetBool("wakeUp", true);
        }
        healthBar.maxValue = maxHealth.initialValue; // Set the maximum value of the health bar
        healthBar.value = maxHealth.initialValue; // Initialize the health bar to the maximum value
    }

    private void Awake()
    {
        health = maxHealth.initialValue; // Initialize health to the maximum value
    }


    public void takeDamage(float damage)
    {
        StartCoroutine(BeingHit()); // Trigger stagger animation
        health -= damage; // Reduce health by the damage amount
        if (!healthBar.gameObject.activeInHierarchy)
        {
            healthBar.gameObject.SetActive(true); // Ensure the health bar is active
        }
        health = Mathf.Clamp(health, 0, maxHealth.initialValue); // Ensure health does not go below zero
        healthBar.value = health; // Update the health bar UI
        Debug.Log($"{enemyName} took {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            isDead.runtimeValue = true; // Set the dead state to true
            DeathEffect(); // Trigger death effect
            healthBar.gameObject.SetActive(false); // Hide the health bar
            this.gameObject.SetActive(false); // Deactivate the enemy if health is zero or below
        }
    }

    private void DeathEffect()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f); // Destroy the effect after 1 second
        }
    }


    private IEnumerator BeingHit()
    {
        Animation.SetBool("beingHit", true);
        yield return new WaitForSeconds(0.5f); // Adjust the wait time as needed for the stagger animation
        Animation.SetBool("beingHit", false);
    }

    public void Doge()
    {
        StartCoroutine(DodgeCo());
    }

    private IEnumerator DodgeCo()
    {
        Animation.SetBool("doge", true);
        yield return new WaitForSeconds(0.6f); // Adjust the wait time as needed for the dodge animation
        Animation.SetBool("doge", false);
    }
}
