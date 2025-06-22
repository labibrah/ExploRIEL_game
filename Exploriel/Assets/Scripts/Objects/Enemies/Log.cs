using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Log : Enemy
{


    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
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
            StartFighting();


        }
        else
        {
            ChangeState(EnemyState.Idle);
            Animation.SetBool("wakeUp", false);
        }
    }



    private void SetAnimFloat(Vector2 direction)
    {
        Animation.SetFloat("moveX", direction.x);
        Animation.SetFloat("moveY", direction.y);
    }

    public void changeAnim(Vector2 direction)
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

    public void StartFighting()
    {
        player.GetComponent<PlayerExploring>().StartingPosition.runtimeValue = target.position;
        SceneTracker.Instance.RecordSceneAndPosition(player.transform.position);
        SceneManager.LoadScene(SceneToFight);
    }
}
