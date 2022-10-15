using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellRangeList : MonoBehaviour
{
    List<Transform> enemiesInRange = new List<Transform>();
    [SerializeField] private GameObject spellPrefab = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.GetComponent<Enemy>() && other.isTrigger)
        {
            enemiesInRange.Add(other.GetComponent<Transform>());
        }
       foreach(Transform position in enemiesInRange)
        {
            var spell = Instantiate(spellPrefab).GetComponent<PositionFollower>();
            spell.target =position;
        }
        enemiesInRange.Clear();
    }
}
