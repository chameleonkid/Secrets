using System.Collections;
using UnityEngine;

public class DamageOverTime : Hitbox
{
    [Tooltip("How many times should tick damage be applied?")]
    [SerializeField] private int ticks = 3;
    [Tooltip("How long is the duration between ticks?")]
    [SerializeField] private float tickDuration = 1;
    [Tooltip("How much tick damage to apply on each tick?")]
    [SerializeField] private float tickDamage = 1;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Character>();
        if (hit != null && hit.gameObject.activeInHierarchy)
        {
            hit.StartCoroutine(DamageOverTimeCo(hit));
        }
    }

    private IEnumerator DamageOverTimeCo(Character hit)
    {
        DamagePopUpManager.RequestDamagePopUp(tickDamage, hit.transform);
        hit.health -= tickDamage; // Tick once on hit
        for (int i = 1; i < ticks; i++)
        {
            if (hit != null)
            {
                yield return new WaitForSeconds(tickDuration);
                DamagePopUpManager.RequestDamagePopUp(tickDamage, hit.transform);
                hit.health -= tickDamage;
            }
        }
    }
}
