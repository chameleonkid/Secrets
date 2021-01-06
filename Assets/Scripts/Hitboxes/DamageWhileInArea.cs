using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWhileInArea : Hitbox
{
    [SerializeField] private List<Character> charactersInAreaList = default;

    [Tooltip("How long is the duration between ticks?")]
    [SerializeField] private float tickDuration = 1;
    [Tooltip("How much tick damage to apply on each tick?")]
    [SerializeField] private float tickDamage = 1;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Character>();
        if (hit != null && hit.gameObject.activeInHierarchy)
        {
            StartCoroutine(TakeAoeDamage(hit));
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

    private IEnumerator TakeAoeDamage(Character hit)
    {
        while (charactersInAreaList.Contains(hit))
        {
            if (hit != null)
            {
                DamagePopUpManager.RequestDamagePopUp(tickDamage, hit.transform);
                hit.health -= tickDamage;                                           // This means DMG WITHOUT ARMOR and No iFrames!
                yield return new WaitForSeconds(tickDuration);
                Debug.Log("Character took dmg from AOE");
            }
        }
    }
}
