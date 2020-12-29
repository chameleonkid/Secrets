using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBattleTower : MonoBehaviour
{
    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }

    [SerializeField] private ColliderTrigger colliderTrigger = default;
    [SerializeField] private Enemy enemy1 = default;
    [SerializeField] private Enemy enemy2 = default;
    [SerializeField] private Enemy enemy3 = default;
    [SerializeField] private Enemy bossMinion = default;
    [SerializeField] private List<Vector3> spawnPostionList = default;
    [SerializeField] private Enemy boss = default;
    [SerializeField] private GameObject bossGameObject = default;
    [SerializeField] private Stage stage = default;
    [SerializeField] private List<Enemy> spawnedEnemiesList = default;
    [SerializeField] private int minionsToSpawn;                //set the minions to spawn in inspector
    [SerializeField] private int spawnedMinionsCounter = 0;

    [SerializeField] private bool minionsSpawned = false;
    [SerializeField] private Shield bossshield = default;

    [SerializeField] private bool isDefeated;
    [SerializeField] private BoolValue storeDefeated;




    private void Awake()
    {
        isDefeated = storeDefeated.RuntimeValue;
        if (isDefeated)
        {
            bossGameObject.SetActive(false);
        }
        else
        {
            bossGameObject.SetActive(true);
            spawnPostionList = new List<Vector3>();
            foreach (Transform spawnPoint in transform.Find("SpawnPoints"))
            {
                spawnPostionList.Add(spawnPoint.position);
            }

            stage = Stage.WaitingToStart;
        }
    }

    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += EnterBossArea;       //Subscribe to not start the Battle multiple Times
        boss.OnEnemyTakeDamage += BossTakesDamage;
        boss.OnEnemyDied += BossDied;
    }

    private void BossDied()
    {
        Debug.Log("The Boss died!");
        DestroyAllEnemies();
        storeDefeated.RuntimeValue = true;
        CancelInvoke("SpawnEnemy");                                                          // Stop spawning enemies
    }

    private void BossTakesDamage()
    {
        Debug.Log("Boss took DMG!");
        switch (stage)
        {
            case Stage.Stage_1:
                   if(boss.GetPercentHealth() <= 70)
                {
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if (boss.GetPercentHealth() <= 50)
                {
                    StartNextStage();
                }
                break;
        }
    }

    private void EnterBossArea()
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= EnterBossArea;       //Unsubscribe to not start the Battle multiple Times
    }

    private void StartBattle()
    {
        Debug.Log("BossBattle has started!");
        StartNextStage();
        InvokeRepeating("SpawnEnemy", 1.0f, 10.0f);                                          //Call that function in 1 second every second
    }

    private void SpawnEnemy()
    {
        int rndEnemy = Random.Range(0, 100);
        Enemy minion;

        if (stage == Stage.Stage_1)
        {

            Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];

            if (rndEnemy < 35)
            {
               minion = Instantiate(enemy1, spawnPoint, Quaternion.identity);
            }
            if (rndEnemy >= 35 && rndEnemy <= 75)
            {
                minion = Instantiate(enemy2, spawnPoint, Quaternion.identity);
            }
            else
            {
                minion = Instantiate(enemy3, spawnPoint, Quaternion.identity);
            }
            spawnedEnemiesList.Add(minion);
        }
        if (stage == Stage.Stage_2)
        {
            if (minionsSpawned == false)                                    //Spawn X RED SHIELD-MINIONS
            {
                for (int i = 0; i <= minionsToSpawn; i++)    
                {
                    Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
                    minion = Instantiate(bossMinion, spawnPoint, Quaternion.identity);
                    minion.isMinion = true;
                    minion.OnMinionDied += MinionKilled;
                    spawnedMinionsCounter++;
                }
                minionsSpawned = true;
            }
            else
            {
                {
                    Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
                    minion = Instantiate(enemy1, spawnPoint, Quaternion.identity);
                    spawnedEnemiesList.Add(minion);
                }
            }
        }
    }

    private void MinionKilled()
    {
        spawnedMinionsCounter--;
        if(spawnedMinionsCounter <= 0)
        {
            bossshield.triggerShield();
        }
    }

    private void DestroyAllEnemies()
    {
        foreach (Enemy enemy in spawnedEnemiesList)
        {
            enemy.KillEnemy();
        }
    }

    private void StartNextStage()
    {
        switch(stage)
        {
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                bossshield.triggerShield();
                Debug.Log("Shield should be active now!");
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;     
        }
        Debug.Log("Next Stage has started " + stage);
    }



    

}
