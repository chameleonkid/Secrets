using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampWitchBattle : MonoBehaviour
{

    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }


    [Header("TriggerArea")]
    [SerializeField] private PlayerEventTrigger playerTrigger = default;
    [Header("Potential Enemies")]
    [SerializeField] private Enemy enemy1 = default;
    [SerializeField] private Enemy enemy2 = default;
    [SerializeField] private Enemy enemy3 = default;
    [Header("Spawnrate in seconds")]
    [SerializeField] private float spawnRate = 1;
    [Header("List of spawned Enemies")]
    [SerializeField] private List<Enemy> spawnedEnemiesList = default;
    [Header("Spawn Positions")]
    [SerializeField] private List<Vector3> spawnPostionList = default;
    [Header("The Boss")]
    [SerializeField] private SwampWitchBoss boss = default;
    [SerializeField] private SwampWitchBoss bossTwo = default;
    [SerializeField] private GameObject bossGameObject = default;
    [SerializeField] private GameObject bossTwoGameObject = default;
    [SerializeField] private GameObject bossTwoPhaseTwoProjectile = default;
    [SerializeField] private Collider2D bossHurtBox = default;
    [SerializeField] private Collider2D bossTwoHurtBox = default;
    [SerializeField] private int bossKillCounter = 0;
    [Header("TriggerArea")]
    [SerializeField] private GameObject leaveBlock = default;
    [Header("BackPortal")]
    [SerializeField] private GameObject portal = default;


    [Header("Stages")]
    [SerializeField] private Stage stage = default;

    [Header("Saves")]
    [SerializeField] private bool isDefeated;
    [SerializeField] private BoolValue storeDefeated;

    [Header("Sound")]
    [SerializeField] private AudioClip startBattleSound;
    [SerializeField] private AudioClip[] endBattleMusic;
    [SerializeField] private AudioClip bossDiedSound;

    [Header("TriggerArea")]
    [SerializeField] private GameObject triggerArea;


    private void Awake()
    {
        leaveBlock.SetActive(false);
        isDefeated = storeDefeated.RuntimeValue;
        if (isDefeated)
        {
            portal.SetActive(true);
            bossGameObject.SetActive(false);
            bossTwoGameObject.SetActive(false);
            triggerArea.SetActive(false);

        }
        else
        {
            portal.SetActive(false);
            bossGameObject.SetActive(true);
            bossTwoGameObject.SetActive(true);
            spawnPostionList = new List<Vector3>();
            foreach (Transform spawnPoint in transform.Find("SpawnPoints"))
            {
                spawnPostionList.Add(spawnPoint.position);
            }

            stage = Stage.WaitingToStart;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTrigger.OnTriggerEnter += EnterBossArea;       //Subscribe to not start the Battle multiple Times
        boss.OnEnemyTakeDamage += BossTakesDamage;
        bossTwo.OnEnemyTakeDamage += BossTakesDamage;
        boss.OnEnemyDied += BossDied;
        bossTwo.OnEnemyDied += BossDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BossTakesDamage()
    {
        //TAKE CARE! THIS HAPPENS 4 TIMES. START WITH HIGH COOLDOWNS!
        Debug.Log("Boss took DMG!");
        switch (stage)
        {
            case Stage.Stage_1:
                if (boss.GetPercentHealth() <= 70 || bossTwo.GetPercentHealth() <= 70)
                {
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if (boss.GetPercentHealth() <= 50 || bossTwo.GetPercentHealth() <= 50)
                {
                    bossTwo.projectile = bossTwoPhaseTwoProjectile;
                    bossTwo.setAmountOfPojectiles(3);
                    bossTwo.setTimeBetweenProjectiles(0.5f);

                    StartNextStage();
                }
                break;
        }
    }

    private void BossDied()
    {
        bossKillCounter++;
        Debug.Log("One Boss died!");
        SoundManager.RequestSound(bossDiedSound);
        if (bossKillCounter == 2)
        {
            portal.SetActive(true);
            leaveBlock.SetActive(false);
            MusicManager.RequestMusic(endBattleMusic);
            DestroyAllEnemies();
            storeDefeated.RuntimeValue = true;
            CancelInvoke("SpawnEnemy");
        }
                                                       // Stop spawning enemies
    }

    private void DestroyAllEnemies()
    {
        foreach (Enemy enemy in spawnedEnemiesList)
        {
            enemy.KillEnemy();
        }
    }



    private void EnterBossArea()
    {
        if (bossGameObject.activeInHierarchy == true)
        {
            StartBattle();
            playerTrigger.OnTriggerEnter -= EnterBossArea;       //Unsubscribe to not start the Battle multiple Times
            triggerArea.SetActive(false);
            leaveBlock.SetActive(true);
        }
    }

    private void StartBattle()
    {
        Debug.Log("BossBattle has started!");
        boss.animator.SetTrigger("StartBattle");
        bossTwo.animator.SetTrigger("StartBattle");
        StartCoroutine(ActivateBossValuesCo());
        StartNextStage();
        InvokeRepeating("SpawnEnemy", 1.0f, spawnRate);
    }


    private void StartNextStage()
    {
        switch (stage)
        {
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                EnhanceAttacks();     
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;
        }
        Debug.Log("Next Stage has started " + stage);
    }

    private void EnhanceAttacks()
    {
        //  var boss = this.GetComponent<BossPumpkin>();
        boss.HalfCooldown();
        bossTwo.HalfCooldown();
        boss.HalfCooldownSpellTwo();
        bossTwo.HalfCooldownSpellTwo();
    }

    private IEnumerator ActivateBossValuesCo()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.RequestSound(startBattleSound);
        yield return new WaitForSeconds(3f);
        bossHurtBox.enabled = true;
        bossTwoHurtBox.enabled = true;
        boss.GetComponent<SwampWitchBoss>().enabled = true;
        bossTwo.GetComponent<SwampWitchBoss>().enabled = true;
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

        else
        {
           Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
           minion = Instantiate(enemy1, spawnPoint, Quaternion.identity);
           spawnedEnemiesList.Add(minion);
            
        }
     }
}



