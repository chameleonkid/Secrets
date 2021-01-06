using UnityEngine;

public class MovingTurrets : TurretEnemy
{
    [SerializeField] private float minDistance;

    protected override void FixedUpdate()
    {
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius)
        {
            if (currentState == State.idle || currentState == State.walk && currentState != State.stagger)
            {
                if (canFire)
                {
                    currentState = State.attack;
                    FireProjectile();
                }
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                SetAnimatorXYSingleAxis(temp - transform.position);
                rigidbody.MovePosition(temp);
                currentState = State.walk;
                animator.SetBool("isMoving", true);

                var newPosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rigidbody.MovePosition(newPosition);
            }
        }
        else if (distance > chaseRadius)
        {
            OutsideChaseRadiusUpdate();
        }
    }
}
