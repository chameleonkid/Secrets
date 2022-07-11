using UnityEngine;

public class PatrolEnemy : SimpleEnemy
{
    public Transform[] pathes;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    protected override void OutsideChaseRadiusUpdate()
    {
        animator.SetBool("isMoving", true);
        var distance = Vector3.Distance(transform.position, pathes[currentPoint].position);
        if (distance > roundingDistance)
        {
            var newPosition = Vector3.MoveTowards(transform.position, pathes[currentPoint].position, moveSpeed * Time.deltaTime);
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
        if (currentPoint == pathes.Length - 1)
        {
            currentPoint = 0;
            currentGoal = pathes[0];
        }
        else
        {
            currentPoint++;
            currentGoal = pathes[currentPoint];
        }
    }
}
