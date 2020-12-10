using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
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
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            GetComponent<Collider2D>().enabled = false; // Disable this collider to prevent from affecting multiple enemies
            StartCoroutine(DamageOverTimeCo(enemy));
        }
    }

    private IEnumerator DamageOverTimeCo(Enemy enemy)
    {
        enemy.health -= tickDamage; // Tick once on hit
        for (int i = 1; i < ticks; i++)
        {
            if (enemy != null)
            {
                yield return new WaitForSeconds(tickDuration);
                enemy.health -= tickDamage;
                DamagePopUpManager.RequestDamagePopUp(tickDamage, enemy.transform);
            }
        }
        Destroy(this.gameObject);
    }
}
