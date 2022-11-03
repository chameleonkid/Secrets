using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : TurretEnemy
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private int amountOfEnemiesPerWave;
    [SerializeField] float spawnOffsetY;
    [SerializeField] float spawnOffsetX;
    [SerializeField] float spawnRange;
    [SerializeField] float timeBetweenEnemies;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float delayLeft;
    [SerializeField] bool canSpawn;
    [SerializeField] bool isInSpawnRange;
    [SerializeField] bool isDestroyed = false;
    [SerializeField] Collider2D hurtBox;




    protected override void FixedUpdate()
    {
        isInSpawnRange = IsInSpawnRange();
        delayLeft -= Time.deltaTime;
        if (delayLeft <= 0)
        {
            canSpawn = true;
            delayLeft = timeBetweenWaves;
        }
        if (canSpawn == true && isDestroyed == false)
        {
            canSpawn = false;
            if(isInSpawnRange == true)
            {
                animator.SetTrigger("TriggerSpawn");
            }

        }
    }


    protected bool IsInSpawnRange()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= spawnRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SpawnWave()
    {
        StartCoroutine(SpawnWaveCo());  //is called in the animation!
    }

    protected virtual IEnumerator SpawnWaveCo()
    {
       Vector3 spawnPos = transform.position;
       spawnPos.x += spawnOffsetX;
       spawnPos.y += spawnOffsetY;
       for (int i = 0; i < amountOfEnemiesPerWave; i++)
       {
            Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenEnemies);
       }
    }

    protected override void Die()
    {
        if (deathSounds.Length >= 0)
        {
            SoundManager.RequestSound(deathSounds.GetRandomElement());
        }

        levelSystem.AddExperience(enemyXp);

        if (roomSignal != null)
        {
            roomSignal.Raise();
        }

        isDestroyed = true;
        animator.SetTrigger("TriggerDestroy");
        CheckForMinion();
        hurtBox.enabled = false;
    }

}
