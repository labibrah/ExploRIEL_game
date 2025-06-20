using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger
}


public class PlayerExploring : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D myRigidbody;
    public UnityEngine.Vector3 change = UnityEngine.Vector3.zero; // Use UnityEngine.Vector3 to match the original code
    private Animator animator;
    public FloatValue currentHealth;
    public PlayerState currentState;
    public Signal playerHealthSignal;
    public Signal playerAttackSignal;
    public VectorValue StartingPosition;
    public Inventory inventory;
    public SpriteRenderer receiveItemSprite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        myRigidbody.position = StartingPosition.runtimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return; // If the player is interacting, do not process movement
        }
        change = UnityEngine.Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        change.z = 0;
        change.Normalize(); // Normalize the vector to prevent faster diagonal movement

        if (Input.GetMouseButtonDown(0) && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }

    private void UpdateAnimationAndMove()
    {
        if (change != UnityEngine.Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            myRigidbody.MovePosition(myRigidbody.position + new UnityEngine.Vector2(change.x, change.y) * speed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    public void RaiseItem()
    {
        if (currentState != PlayerState.interact)
        {
            animator.SetBool("receive_item", true);
            currentState = PlayerState.interact;
            receiveItemSprite.sprite = inventory.currentItem.itemSprite;
        }
        else
        {
            animator.SetBool("receive_item", false);
            currentState = PlayerState.walk;
            receiveItemSprite.sprite = null; // Clear the sprite when not receiving an item
        }

    }


    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return new WaitForSeconds(0.26f); // Wait for the attack animation to play
        animator.SetBool("attacking", false);
        currentState = PlayerState.walk;
    }
}
