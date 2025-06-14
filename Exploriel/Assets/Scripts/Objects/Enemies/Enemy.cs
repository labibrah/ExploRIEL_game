using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger,
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    private float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffectPrefab;
    public Rigidbody2D rb;
    public Transform target;
    public GameObject player;
    public float chaseRaidus;
    public float attackRadius;
    public Transform homePosition;
    public Animator Animation;
    public Slider healthBar; // Reference to the health bar UI element
    public bool isFighting; // Flag to indicate if the enemy is in a fight
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
        if (Animation != null || isFighting)
        {
            Animation.SetBool("wakeUp", true);
        }
        healthBar.maxValue = maxHealth.initialValue; // Set the maximum value of the health bar
        healthBar.value = maxHealth.initialValue; // Initialize the health bar to the maximum value
    }

    private void Awake()
    {
        health = maxHealth.initialValue; // Initialize health to the maximum value
        currentState = EnemyState.Idle; // Set the initial state to Idle
    }
    public void Knock(Rigidbody2D enemy, float knockbackDuration, float damage)
    {
        StartCoroutine(KnockbackCoroutine(enemy, knockbackDuration));
        takeDamage(damage); // Apply damage to the enemy
    }

    private IEnumerator KnockbackCoroutine(Rigidbody2D enemy, float knockbackDuration)
    {
        if (enemy != null)
        {
            enemy.GetComponent<Enemy>().currentState = EnemyState.Stagger; // Set the enemy state to stagger
            yield return new WaitForSeconds(knockbackDuration); // Wait for the knockback duration
            enemy.GetComponent<Enemy>().currentState = EnemyState.Idle; // Reset the enemy state to idle
            enemy.velocity = Vector2.zero; // Ensure the enemy's velocity is reset
        }
    }

    public void takeDamage(float damage)
    {
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

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            //Animation.SetInteger("state", (int)currentState);
        }
    }
}
