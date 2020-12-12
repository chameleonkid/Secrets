using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Hitbox : MonoBehaviour
{
    [Tooltip("Enable this if the hitbox collider should only affect one hurtbox in its lifetime.")]
    [SerializeField] private bool disableWhenHit = false;
    private new Collider2D collider;

    private void Awake() => collider = GetComponent<Collider2D>();

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (HitTarget(other.gameObject))            //thoughts: is true but doesnt give the Tag as parameter
            {
                OnHit(other);                           // Calls OnHit which will trigger DamageOnTrigger in my case with "other" as parameter
                collider.enabled = !disableWhenHit;
            }
        }
    }

    protected abstract void OnHit(Collider2D other);

    protected bool HitTarget(GameObject other)
    {
        var hitbox = this.gameObject;
        var isPlayerHitbox = hitbox.CompareTag("Player") || hitbox.CompareTag("arrow") || hitbox.CompareTag("spell"); // Whatever is true first
        var isEnemyHitbox = hitbox.CompareTag("enemy") || hitbox.CompareTag("enemySpell");

        var hitPlayer = other.CompareTag("Player"); //Boolean
        var hitEnemy = other.CompareTag("enemy");   // Boolean

        return (isPlayerHitbox && hitEnemy) || (isEnemyHitbox && hitPlayer);
    }
}
