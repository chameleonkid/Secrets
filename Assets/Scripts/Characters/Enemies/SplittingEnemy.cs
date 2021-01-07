using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingEnemy : Enemy
{
    [SerializeField] private int maxSplit = 0;
    [SerializeField] private Enemy enemiesToSpawn = default;

    protected override void Die()
    {
        StartCoroutine(SpawnOnKillCo());

    }

    IEnumerator SpawnOnKillCo()
    {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Derived Class wurde ausgeführt DIE wurde ausgeführt");
        var temp = Random.Range(1f, maxSplit);
        Debug.Log(temp + " Enemies will be spawned");
        for (int i = 0; i <= temp; i++)
        {
            var tempSpawn = this.transform.position;
            tempSpawn += (GetRandomDirection() * UnityEngine.Random.Range(1f, 1f));
            Debug.Log(i + "st Enemy spawned");
            Instantiate(enemiesToSpawn, tempSpawn, Quaternion.identity);
        }
        base.Die();
    }


}
