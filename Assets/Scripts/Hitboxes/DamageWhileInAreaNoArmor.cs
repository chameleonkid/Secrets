using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWhileInAreaNoArmor : DamageWhileInArea
{
    protected override IEnumerator TakeAoeDamage(Character hit)
    {
        while (charactersInAreaList.Contains(hit))
        {
            if (hit != null)
            {
                hit.health -= tickDamage;                                           // This means DMG WITHOUT ARMOR and No iFrames!
                DamagePopUpManager.RequestDamagePopUp(tickDamage, hit.transform);
                yield return new WaitForSeconds(tickDuration);
                Debug.Log("Character took dmg from AOE");
            }
        }
    }
}
