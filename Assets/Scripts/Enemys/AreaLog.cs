using UnityEngine;

public class AreaLog : EnemyLog
{
    public Collider2D boundary;

    protected override void FixedUpdate()
    {
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius
            && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                SetAnimatorXYSingleAxis(temp - transform.position);
                rigidbody.MovePosition(temp);
                currentState = EnemyState.walk;
                animator.SetBool("WakeUp", true);
            }
        }
        if (distance > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            animator.SetBool("WakeUp", false);
            // ChangeState(EnemyState.idle);
        }
    }
}
