using UnityEngine;

public class DamageOnTrigger : Hitbox
{
    [SerializeField] private float _damage = 1;                     // default value if not overwritten by other classes/scripts
    public float damage { get => _damage; set => _damage = value; }
    public bool isCritical { get; set; } = false;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Character>();
        if (hit != null)
        {
            var dmg = isCritical ? damage * 2 : damage;
            hit.TakeDamage(dmg, isCritical);
         //   Debug.Log(this + " has hit " + other);
        }
    }
}
