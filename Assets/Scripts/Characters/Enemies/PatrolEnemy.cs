using UnityEngine;

public class PatrolEnemy : SimpleEnemy
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    protected override void OutsideChaseRadiusUpdate()
    {
        animator.SetBool("isMoving", true);
        var distance = Vector3.Distance(transform.position, path[currentPoint].position);
        if (distance > roundingDistance)
        {
            var newPosition = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
            SetAnimatorXYSingleAxis(newPosition - transform.position);
            rigidbody.MovePosition(newPosition);
        }
        else
        {
            ChangeGoal();
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
