using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyLog
{
    protected override void FixedUpdate()
    {
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                SetAnimatorXYSingleAxis(temp - transform.position);
                rigidbody.MovePosition(temp);
                currentState = EnemyState.walk;
                animator.SetBool("Moving", true);
            }

        }
        else if (distance <= chaseRadius && distance <= attackRadius)
        {
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
        else if (distance >= chaseRadius && distance >= attackRadius)
        {
            currentState = EnemyState.idle;
            animator.SetBool("Moving", false);
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        animator.SetBool("Attacking", true);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.5f); //Attack CD
        currentState = EnemyState.walk;
    }
}
