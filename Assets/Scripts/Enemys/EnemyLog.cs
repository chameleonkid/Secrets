public class EnemyLog : Enemy
{
    protected virtual void Start() => animator.SetBool("WakeUp", true);

    protected override void OutsideChaseRadiusUpdate()
    {
        animator.SetBool("WakeUp", false);
        // ChangeState(EnemyState.idle);
    }
}
