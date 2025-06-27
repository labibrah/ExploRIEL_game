using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EnemyState
{
    Idle,
    Walk
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
    public BoolValue isDead;
    public string SceneToFight;
    public TextMeshProUGUI enemyNameText;

    public virtual void Start()
    {
        if (isDead.runtimeValue)
        {
            gameObject.SetActive(false); // Deactivate the enemy if it is dead
            return;
        }
        currentState = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
        if (Animation != null)
        {
            Animation.SetBool("wakeUp", true);
        }
        if (enemyNameText != null)
        {
            enemyNameText.text = enemyName; // Set the enemy name in the UI
        }
    }

    private void Awake()
    {
        health = maxHealth.initialValue; // Initialize health to the maximum value
        currentState = EnemyState.Idle; // Set the initial state to Idle
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

    private IEnumerator BeingHit()
    {
        Animation.SetBool("beingHit", true);
        yield return new WaitForSeconds(0.5f); // Adjust the wait time as needed for the stagger animation
        Animation.SetBool("beingHit", false);
        ChangeState(EnemyState.Walk);
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
