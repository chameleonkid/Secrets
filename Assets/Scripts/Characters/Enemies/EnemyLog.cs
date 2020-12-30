public class EnemyLog : Enemy
{
    protected virtual void Start() => animator.SetBool("WakeUp", true);

    protected override void OutsideChaseRadiusUpdate()
    {
        animator.SetBool("isMoving", false);
        // ChangeState(EnemyState.idle);
    }
}
