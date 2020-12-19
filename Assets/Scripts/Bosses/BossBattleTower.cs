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

    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private Enemy enemy;
    [SerializeField] private List<Vector3> spawnPostionList;
    [SerializeField] private Enemy boss;
    [SerializeField] private Stage stage;
    [SerializeField] private List<Enemy> spawnedEnemiesList;



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
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;       //Subscribe to not start the Battle multiple Times
        boss.OnEnemyTakeDamage += Boss_OnEnemyTakeDamage;
        boss.OnEnemyDied += Boss_OnEnemyDied;                                               
    }

    private void Boss_OnEnemyDied()
    {
        Debug.Log("The Boss died!");
        DestroyAllEnemies();
        CancelInvoke("SpawnEnemy");                                                          // Stop spawning enemies
    }

    private void Boss_OnEnemyTakeDamage()
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

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;       //Unsubscribe to not start the Battle multiple Times
    }

    private void StartBattle()
    {
        Debug.Log("BossBattle has started!");
        StartNextStage();
        InvokeRepeating("SpawnEnemy", 1.0f, 1.0f);                                          //Call that function in 1 second every second
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
        Enemy minion = Instantiate(enemy, spawnPoint, Quaternion.identity);
        spawnedEnemiesList.Add(minion);
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
