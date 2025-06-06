using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedLog : Log
{
    public BoxCollider2D boundsCollider;

    public override void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector2 playerPos2D = new Vector2(target.position.x, target.position.y);
        if (distance <= chaseRaidus && distance > attackRadius && boundsCollider.OverlapPoint(playerPos2D))
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
}
