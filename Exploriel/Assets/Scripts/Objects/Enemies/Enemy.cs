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
    public GameObject deathEffectPrefab;
    public Rigidbody2D rb;
    public Transform target;
    public float chaseRaidus;
    public float attackRadius;
    public Transform homePosition;
    public Animator Animation;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
        if (Animation != null)
        {
            Animation.SetBool("wakeUp", true);
        }
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

    private void takeDamage(float damage)
    {
        health -= damage; // Reduce health by the damage amount
        if (health <= 0)
        {
            DeathEffect(); // Trigger death effect
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
