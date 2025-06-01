using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D rb;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRaidus && distance > attackRadius)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rb.MovePosition(temp);
                changeAnim(temp - transform.position);
                ChangeState(EnemyState.Walk);
                Animation.SetBool("wakeUp", true);
            }

        }
        else if (distance <= attackRadius)
        {
            //AttackTarget();
            //Debug.Log("Attacking the target");
        }
        else
        {
            ChangeState(EnemyState.Idle);
            Animation.SetBool("wakeUp", false);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            //Animation.SetInteger("state", (int)currentState);
        }
    }

    private void SetAnimFloat(Vector2 direction)
    {
        Animation.SetFloat("moveX", direction.x);
        Animation.SetFloat("moveY", direction.y);
    }

    private void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(new Vector2(1, 0));
            }
            else
            {
                SetAnimFloat(new Vector2(-1, 0));
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(new Vector2(0, 1));
            }
            else
            {
                SetAnimFloat(new Vector2(0, -1));
            }
        }
        else
        {
            SetAnimFloat(Vector2.zero);
        }

    }
}
