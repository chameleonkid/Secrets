using UnityEngine;
using Pathfinding;

public class RandomMoveEnemy : Enemy // or Character, depending on your base class
{
    protected void Start() => animator.SetBool("isMoving", false);

    protected override void OutsideChaseRadiusUpdate()
    {
        randomMovement();
        leftChaseRadius = true;
    }

    protected override void InsideChaseRadiusUpdate()
    {
        randomMovement();
        leftChaseRadius = true;
    }

}
