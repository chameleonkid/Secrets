using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float attackCoolDown = 0.5f;
    protected override void FixedUpdate()
    {
        //! Temporary!
        if (currentState is Schwer.States.Knockback) {
            currentState.FixedUpdate();
            return;
        }
        else {
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
            if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk && currentStateEnum != StateEnum.stagger)
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
            if ((currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk) && currentStateEnum != StateEnum.stagger)
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
        currentStateEnum = StateEnum.attack;
        animator.SetBool("Attacking", true);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(attackCoolDown); //Attack CD
        currentStateEnum = StateEnum.walk;
    }
}
