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
    [SerializeField] private GameObject blueBlob = default;
    [SerializeField] private GameObject greenBlob = default;
    [SerializeField] private int numberOfEnemiesToSpawn = 1;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [Header("Interval between Spawnwaves")]
    [SerializeField] private float spawnRate = 1;
    [Header("List of spawned Enemies")]
    [SerializeField] private List<Enemy> spawnedEnemiesList = default;
    [Header("Spawn Positions")]
    [SerializeField] private List<Vector3> spawnPostionList = default;
    [SerializeField] private GameObject[] spawnPostionLights = default;
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
    [Header("FinalRoomBlock")]
    [SerializeField] private GameObject FinalRoomBlock = default;
    [Header("EndOfFightLights")]
    [SerializeField] private GameObject EndOfFightLights = default;


    [Header("Stages")]
    [SerializeField] private Stage stage = default;

    [Header("Saves")]
    [SerializeField] private bool isDefeated;
    [SerializeField] private BoolValue storeDefeated;

    [Header("Sound")]
    [SerializeField] private AudioClip startBattleSound;
    [SerializeField] private AudioClip[] endBattleMusic;
    [SerializeField] private AudioClip bossDiedSound;
    [SerializeField] private AudioClip spawnEnemysound;

    [Header("TriggerArea")]
    [SerializeField] private GameObject triggerArea;


    private void Awake()
    {
        leaveBlock.SetActive(false);
        isDefeated = storeDefeated.RuntimeValue;
        if (isDefeated)
        {
            FinalRoomBlock.SetActive(false);
            bossGameObject.SetActive(false);
            bossTwoGameObject.SetActive(false);
            triggerArea.SetActive(false);
            EndOfFightLights.SetActive(true);
        }
        else
        {
            FinalRoomBlock.SetActive(true);
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
            FinalRoomBlock.SetActive(false);
            EndOfFightLights.SetActive(true);
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
        InvokeRepeating("SpawnEnemy", 10.0f, spawnRate);
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
        StartCoroutine(SpawnEnemiesCo());
    }

    private IEnumerator SpawnEnemiesCo()
    {
        GameObject minion;
        Vector2 spawnPointGreen = spawnPostionList[0];
        Vector2 spawnPointBlue = spawnPostionList[1];
        spawnPostionLights[0].SetActive(true);
        spawnPostionLights[1].SetActive(true);
        SoundManager.RequestSound(spawnEnemysound);
        yield return new WaitForSeconds(2f);
        for (int i=0; i<numberOfEnemiesToSpawn;i++)
        {
            minion = Instantiate(blueBlob, spawnPointBlue, Quaternion.identity);
            Destroy(minion, 12f);
            minion = Instantiate(greenBlob, spawnPointGreen, Quaternion.identity);
            Destroy(minion, 12f);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        spawnPostionLights[0].SetActive(false);
        spawnPostionLights[1].SetActive(false);

    }
}



