using UnityEngine;

public class RangeEnemy : TurretEnemy
{
    [SerializeField] private float shootingRange = 3;
    [SerializeField] private float escapeRange = 1;

    protected override void FixedUpdate()
    {
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance >= shootingRange)
        {
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
                Vector3 temp = Vector3.MoveTowards(rigidbody.position, path.vectorPath[currentWaipoint], moveSpeed * Time.deltaTime);
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
        else if(distance <= chaseRadius && distance <= shootingRange && distance > escapeRange)
        {
            currentState = State.idle;
            animator.SetBool("isMoving", false);
            if (canAttack)
            {
                currentState = State.attack;
                FireProjectile();
            }
        }
        else if(distance > chaseRadius)
        {
            randomMovement();
        }

        if (distance <= escapeRange)
        {
            AvoidPlayer();
        }

    }

}

