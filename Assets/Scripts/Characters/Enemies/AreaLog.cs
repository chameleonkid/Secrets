using UnityEngine;

public class AreaLog : Enemy
{
    public Collider2D boundary;

    protected void Start() => animator.SetBool("isMoving", false);

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

    protected override void OutsideChaseRadiusUpdate() => animator.SetBool("WakeUp", false);
}
