using UnityEngine;

public class AreaLog : EnemyLog
{
    public Collider2D boundary;

    protected override void InsideChaseRadiusUpdate()
    {
        if (boundary.bounds.Contains(target.transform.position))
        {
            base.InsideChaseRadiusUpdate();
        }
        else
        {
            OutsideChaseRadiusUpdate();
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        animator.SetBool("WakeUp", false);
        // ChangeState(EnemyState.idle);
    }
}
