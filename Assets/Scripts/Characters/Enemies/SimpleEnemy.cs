public class SimpleEnemy : Enemy
{
    protected void Start() => animator.SetBool("isMoving", false);

    protected override void OutsideChaseRadiusUpdate() => randomMovement();
}
