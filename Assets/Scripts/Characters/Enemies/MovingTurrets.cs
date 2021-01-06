using UnityEngine;

public class MovingTurrets : TurretEnemy
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
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    SetAnimatorXYSingleAxis(temp - transform.position);
                    rigidbody.MovePosition(temp);
                    currentState = State.walk;
                    animator.SetBool("isMoving", true);
            }
        }
        else if(distance <= chaseRadius && distance <= shootingRange && distance > escapeRange)
        {
            currentState = State.idle;
            animator.SetBool("isMoving", false);
            if (canFire)
            {
                currentState = State.attack;
                FireProjectile();
            }
        }
        else if(distance > chaseRadius)
        {
            OutsideChaseRadiusUpdate();
        }

        if (distance <= escapeRange)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, -1 * moveSpeed * Time.deltaTime);
            SetAnimatorXYSingleAxis(temp - transform.position);
            rigidbody.MovePosition(temp);
            animator.SetBool("isMoving", true);
        }

    }

}

