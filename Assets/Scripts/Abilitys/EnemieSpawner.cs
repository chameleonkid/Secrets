using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : TurretEnemy
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private int amountOfEnemiesPerWave;
    [SerializeField] float timeBetweenEnemies;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float delayLeft;
    [SerializeField] bool canSpawn;


    protected override void FixedUpdate()
    {
        delayLeft -= Time.deltaTime;
        if (delayLeft <= 0)
        {
            canSpawn = true;
            delayLeft = timeBetweenWaves;
        }
        if (canSpawn == true)
        {
            SpawnWave();
            canSpawn = false;
        }
    }

    void SpawnWave()
    {
        StartCoroutine(SpawnWaveCo());
    }

    protected virtual IEnumerator SpawnWaveCo()
    {
        for (int i = 0; i < amountOfEnemiesPerWave; i++)
        {
            Instantiate(enemyToSpawn, this.transform);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

}
