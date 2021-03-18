using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void FixedUpdate()
    {
        if (currentState != State.stagger)
        {
            rigidbody.velocity = Vector2.zero;
        }
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            if (leftChaseRadius)
            {
                SoundManager.RequestSound(inRangeSounds.GetRandomElement());
            }
            leftChaseRadius = false;
            if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
            {             
                if (path == null)
                {
                    return;
                }
                if (currentWaipoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    return;
                }
                animator.SetBool("isMoving", true);
                Vector3 temp = Vector3.MoveTowards(rigidbody.position, path.vectorPath[currentWaipoint], moveSpeed * speedModifier * Time.deltaTime);
                rigidbody.MovePosition(temp);
                float wayPointdistance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaipoint]);
                SetAnimatorXYSingleAxis(temp - transform.position);

                if (wayPointdistance < nextWaypointDistance)
                {
                    currentWaipoint++;
                }
                else
                {
                    reachedEndOfPath = false;
                }          
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
            leftChaseRadius = true;
            randomMovement();
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


