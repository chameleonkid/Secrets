using UnityEngine;

public class RangeEnemy : TurretEnemy
{
    [SerializeField] private float shootingRange = 3;
    [SerializeField] private float escapeRange = 1;

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
        if (distance <= chaseRadius && distance >= shootingRange)
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
        else if (distance <= chaseRadius && distance <= shootingRange && distance > escapeRange)
        {
            currentStateEnum = StateEnum.idle;
            animator.SetBool("isMoving", false);
            if (canAttack)
            {
                currentStateEnum = StateEnum.attack;
                FireProjectile();
            }
        }
        else if (distance > chaseRadius)
        {
            randomMovement();
            leftChaseRadius = true;
        }

        if (distance <= escapeRange)
        {
            AvoidPlayer();
        }
    }
}
