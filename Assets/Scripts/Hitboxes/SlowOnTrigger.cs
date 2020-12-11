using System.Collections;
using UnityEngine;

public class SlowOnTrigger : Hitbox
{
    [SerializeField] private float slowFactor = 0.5f;
    [SerializeField] private float slowDuration = 2;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Enemy>();
        if (hit != null)
        {
            StartCoroutine(SlowCo(hit));
        }
    }

    private IEnumerator SlowCo(Enemy enemy)
    {
        enemy.moveSpeed *= slowFactor;
        yield return new WaitForSeconds(slowDuration);
        enemy.moveSpeed /= slowFactor;
    }
}
