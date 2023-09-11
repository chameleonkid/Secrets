using UnityEngine;
using Pathfinding;

public class RandomMoveEnemy : Enemy // or Character, depending on your base class
{
    [SerializeField] private Vector3 homePoint;
    protected void Start()
    {
        homePoint = this.transform.position;
        animator.SetBool("isMoving", false);
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        randomMovement();
        leftChaseRadius = true;
    }

    protected override void InsideChaseRadiusUpdate()
    {
        {
            if (this.transform.position != roamingPosition)
            {
                if (walkingTimer > 0)
                {
                    walkingTimer -= Time.deltaTime;
                    Vector3 temp = Vector3.MoveTowards(homePoint, roamingPosition, moveSpeed * speedModifier * Time.deltaTime);
                    SetAnimatorXYSingleAxis(temp - transform.position);
                    rigidbody.MovePosition(temp);
                    currentStateEnum = StateEnum.walk;
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    StartCoroutine(randomWaiting());
                    walkingTimer = 3f;
                }
            }
            else
            {
                if (isWalking == false)
                {
                    StartCoroutine(randomWaiting());
                }
            }
        }
    }

}
