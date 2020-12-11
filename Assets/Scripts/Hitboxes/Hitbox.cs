using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Hitbox : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (HitTarget(other.gameObject))
            {
                OnHit(other);
                return;
            }
        }
    }

    protected abstract void OnHit(Collider2D other);

    protected bool HitTarget(GameObject other)
    {
        var hitbox = this.gameObject;
        var isPlayerHitbox = hitbox.CompareTag("Player") || hitbox.CompareTag("arrow") || hitbox.CompareTag("spell");
        var isEnemyHitbox = hitbox.CompareTag("enemy");

        var hitPlayer = other.CompareTag("Player");
        var hitEnemy = other.CompareTag("enemy");

        return (isPlayerHitbox && hitEnemy) || (isEnemyHitbox && hitPlayer);
    }
}
