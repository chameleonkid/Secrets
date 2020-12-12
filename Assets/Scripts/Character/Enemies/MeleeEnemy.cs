using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyLog
{
    protected override void Start() {}

    protected override void FixedUpdate()
    {
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                SetAnimatorXYSingleAxis(temp - transform.position);
                rigidbody.MovePosition(temp);
                currentState = State.walk;
                animator.SetBool("Moving", true);
            }

        }
        else if (distance <= chaseRadius && distance <= attackRadius)
        {
            if ((currentState == State.idle || currentState == State.walk) && currentState != State.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
        else if (distance >= chaseRadius && distance >= attackRadius)
        {
            currentState = State.idle;
            animator.SetBool("Moving", false);
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = State.attack;
        animator.SetBool("Attacking", true);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.5f); //Attack CD
        currentState = State.walk;
    }
}
