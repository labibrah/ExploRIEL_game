using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingLog : Log
{

    public Transform[] pathPoints;
    public int currentPointIndex = 0;
    public Transform currentGoal;
    public float roundingOffset = 0.3f;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }


    public override void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRaidus && distance > attackRadius)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rb.MovePosition(temp);
                changeAnim(temp - transform.position);
                //ChangeState(EnemyState.Walk);
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

            if (currentGoal == null || Vector3.Distance(transform.position, currentGoal.position) < roundingOffset)
            {
                ChangeGoal();
            }
            Vector3 direction = (currentGoal.position - transform.position).normalized;
            rb.MovePosition(direction * moveSpeed * Time.deltaTime + transform.position);
            changeAnim(direction);

        }
    }

    private void ChangeGoal()
    {
        if (pathPoints.Length == 0) return;

        currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
        currentGoal = pathPoints[currentPointIndex];

        // If the new goal is the same as the current position, skip to the next point
        if (Vector3.Distance(transform.position, currentGoal.position) < roundingOffset)
        {
            ChangeGoal();
        }
    }
}
