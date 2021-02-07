using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
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
                animator.SetBool("isMoving", true);
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
            randomMovement();
        }
    }

    public IEnumerator AttackCo()
    {
        SoundManager.RequestSound(attackSounds.GetRandomElement());
        currentState = State.attack;
        animator.SetBool("Attacking", true);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.5f); //Attack CD
        currentState = State.walk;
    }
}


