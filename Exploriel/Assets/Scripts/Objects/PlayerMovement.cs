using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
}

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    private Rigidbody2D myRigidbody;
    private UnityEngine.Vector2 change;
    private Animator animator;
    public FloatValue currentHealth;
    public PlayerState currentState = PlayerState.walk;
    public Signal playerHealthSignal;
    public VectorValue StartingPosition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        myRigidbody.position = StartingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        change = UnityEngine.Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }


    }

    void UpdateAnimationAndMove()
    {
        if (change != UnityEngine.Vector2.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            myRigidbody.MovePosition(myRigidbody.position + change.normalized * speed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }


    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.5f); // Adjust the wait time as needed for the attack animation
        currentState = PlayerState.walk;
    }

    public void Knock(float knockbackDuration, float damage)
    {
        currentHealth.runtimeValue -= damage; // Reduce the player's health by the damage amount
        if (currentHealth.runtimeValue <= 0)
        {
            this.gameObject.SetActive(false); // Deactivate the player if health is zero or below
        }
        playerHealthSignal.Raise();
        StartCoroutine(KnockbackCoroutine(knockbackDuration));

    }

    private IEnumerator KnockbackCoroutine(float knockbackDuration)
    {

        currentState = PlayerState.stagger;
        yield return new WaitForSeconds(knockbackDuration); // Wait for the knockback duration
        currentState = PlayerState.walk; // Reset the player state to walk
        myRigidbody.velocity = UnityEngine.Vector2.zero; // Reset the velocity of the player
    }
}
