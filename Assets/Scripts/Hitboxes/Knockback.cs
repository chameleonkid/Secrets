using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Hitbox
{
    [SerializeField] private float force = 2;
    [SerializeField] private float duration = 1;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Rigidbody2D>();
        if (hit != null)
        {
            var knockback = hit.transform.position - transform.position;
            knockback.Normalize();
            knockback *= force;
            hit.AddForce(knockback, ForceMode2D.Impulse);
        }
    }
}
