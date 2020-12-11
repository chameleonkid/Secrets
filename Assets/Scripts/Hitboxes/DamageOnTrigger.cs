using UnityEngine;

public class DamageOnTrigger : Hitbox
{
    [SerializeField] private float damage = default;

    protected override void OnHit(Collider2D other)
    {
        // Todo
    }
}
