using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawningLog : Log
{
    // Start is called before the first frame update
    public override void Start()
    {
        if (isDead.runtimeValue)
        {
            isDead.runtimeValue = false; // Reset the dead state
            Destroy(gameObject); // Destroy the enemy if it is dead
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
    }

}
