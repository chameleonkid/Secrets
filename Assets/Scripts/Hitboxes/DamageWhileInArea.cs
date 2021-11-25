using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWhileInArea : Hitbox
{
    [SerializeField] protected List<Character> charactersInAreaList = default;

    [Tooltip("How long is the duration between ticks?")]
    [SerializeField] protected float tickDuration = 1;
    [Tooltip("How much tick damage to apply on each tick?")]
    [SerializeField] protected float tickDamage = 1;
    public bool isCritical { get; set; } = false;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Character>();
        if (hit != null && hit.gameObject.activeInHierarchy)
        {
            if(this)
            {
                StartCoroutine(TakeAoeDamage(hit));
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (HitTarget(other.gameObject))            //thoughts: is true but doesnt give the Tag as parameter
            {
                if (!charactersInAreaList.Contains(other.GetComponent<Character>()))
                {
                    charactersInAreaList.Add(other.GetComponent<Character>());
                }
                OnHit(other);                           // Calls OnHit which will trigger DamageOnTrigger in my case with "other" as parameter
                collider.enabled = !disableWhenHit;
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (charactersInAreaList.Contains(other.GetComponent<Character>()))
        {
            charactersInAreaList.Remove(other.GetComponent<Character>());
        }
    }

    protected virtual IEnumerator TakeAoeDamage(Character hit)
    {
        while (charactersInAreaList.Contains(hit))
        {
            if (hit != null)
            {
                hit.TakeDamage(tickDamage, isCritical);
                hit.RequestGotHitSound();
                yield return new WaitForSeconds(tickDuration);
                Debug.Log("Character took dmg from AOE");
            }
        }
    }
}
