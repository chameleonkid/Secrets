using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpell : MonoBehaviour
{
    [SerializeField] protected float radiusOfSpell;
    protected new Collider2D collider;

    protected virtual void Awake()
    {
        collider = GetComponent<Collider2D>();
    }


    public void OverrideDamage(float damage)
    {
        var hitbox = GetComponent<DamageOnTrigger>();
        if (hitbox != null)
        {
            hitbox.damage = damage;
        }
    }

    public void OverrideDamage(float damage, bool isCritical)
    {
        var hitbox = GetComponent<DamageOnTrigger>();
        if (hitbox != null)
        {
            hitbox.damage = damage;
            hitbox.isCritical = isCritical;
        }
    }

}
