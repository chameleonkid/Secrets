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
    [SerializeField] private Enemy enemy = default;
    [SerializeField] private Enemy enemy2 = default;
    [SerializeField] private List<Vector3> spawnPostionList = default;
    [SerializeField] private Enemy boss = default;
    [SerializeField] private Stage stage = default;
    [SerializeField] private List<Enemy> spawnedEnemiesList = default;



    private void Awake()
    {
        spawnPostionList = new List<Vector3>();
        foreach ( Transform spawnPoint in transform.Find("SpawnPoints"))
        {
            spawnPostionList.Add(spawnPoint.position);
        }

        stage = Stage.WaitingToStart;
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
        CancelInvoke("SpawnEnemy");                                                          // Stop spawning enemies
    }

    private void BossTakesDamage()
    {
        Debug.Log("Boss Took DMG!Check for new Stage");
        switch(stage)
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
        InvokeRepeating("SpawnEnemy", 1.0f, 1.0f);                                          //Call that function in 1 second every second
    }

    private void SpawnEnemy()
    {
        if (stage == Stage.Stage_1)
        {
            Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
            Enemy minion = Instantiate(enemy, spawnPoint, Quaternion.identity);
            spawnedEnemiesList.Add(minion);
        }
        if (stage == Stage.Stage_2)
        {
            Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
            Enemy minion = Instantiate(enemy2, spawnPoint, Quaternion.identity);
            spawnedEnemiesList.Add(minion);
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
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;     
        }
        Debug.Log("Next Stage has started " + stage);
    }



    

}
