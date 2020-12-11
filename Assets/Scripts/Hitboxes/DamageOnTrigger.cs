using UnityEngine;

public class DamageOnTrigger : Hitbox
{
    [SerializeField] private float _damage = 1;
    public float damage { get => _damage; set => _damage = value; }
    public bool isCritical { get; set; } = false;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Character>();
        if (hit != null)
        {
            var dmg = isCritical ? damage * 2 : damage;
            hit.health -= damage;
            DamagePopUpManager.RequestDamagePopUp(dmg, isCritical, hit.transform);
        }
    }
}
