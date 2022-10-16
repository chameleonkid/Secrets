using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellRangeList : MonoBehaviour
{
    List<Transform> enemiesInRange = new List<Transform>();
    [SerializeField] private GameObject spellPrefab = default;
    [SerializeField] private float spellDmg = 1;
    [SerializeField] private bool isCriticalSpell = false;



   private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.GetComponent<Enemy>() && other.isTrigger)
        {
            enemiesInRange.Add(other.GetComponent<Transform>());
        }
       foreach(Transform position in enemiesInRange)
        {
            var spell = Instantiate(spellPrefab).GetComponent<PositionFollower>();
            spell.target = position;
            spell.GetComponentInChildren<AOESpell>().OverrideDamage(spellDmg, isCriticalSpell);
        }
        enemiesInRange.Clear();
    }

    public void SetSpellDmgAndCrit(float spellDamage, bool crit)
    {
        spellDmg = spellDamage;
        isCriticalSpell = crit;
    }
}
