using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

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

    private void takeDamage(float damage)
    {
        health -= damage; // Reduce health by the damage amount
        if (health <= 0)
        {
            this.gameObject.SetActive(false); // Deactivate the enemy if health is zero or below
        }
    }
}
