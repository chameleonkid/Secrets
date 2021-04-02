using Schwer.States;
using UnityEngine;

public class Knockback : Hitbox
{
    [SerializeField] private float force = 3;
    [SerializeField] private float duration = 0.2f;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<ICanKnockback>();
        if (hit != null && !((hit.currentState is PlayerDead) || (hit.currentState is Locked)))
        {
            var knockback = other.transform.position - transform.position;
            knockback.Normalize();
            knockback *= force;
            hit.currentState = new Schwer.States.Knockback(hit, knockback, duration);
        }
    }
}
