using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingEnemy : Enemy
{
    [SerializeField] private int maxSplit = 0;
    [SerializeField] private Enemy enemiesToSpawn = default;

    protected override void Die()
    {
        Debug.Log("Derived Class wurde ausgeführt DIE wurde ausgeführt");
        var temp = Random.Range(1f, maxSplit - 1);
        Debug.Log(temp + " Enemies will be spawned");
        for(int i = 0; i<= temp; i++)
        {
            Debug.Log(i + "st Enemy spawned");
            Instantiate(enemiesToSpawn, this.transform.position, Quaternion.identity);
        }

        base.Die();

    }

}
