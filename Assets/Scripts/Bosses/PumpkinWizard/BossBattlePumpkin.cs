using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattlePumpkin : MonoBehaviour
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
    [Header("Spawnrate in seconds")]
    [SerializeField] private float spawnRate = 1;
    [Header("List of spawned Enemies")]
    [SerializeField] private List<Enemy> spawnedEnemiesList = default;
    [Header("Special Enemies that if killed stop the shield")]
    [SerializeField] private Enemy bossMinion = default;
    [SerializeField] private int minionsToSpawn;                //set the minions to spawn in inspector
    [SerializeField] private int spawnedMinionsCounter = 0;
    [SerializeField] private float timeBetweenMinionSpawn = 0.25f;
    [Header("Spawn Positions")]
    [SerializeField] private List<Vector3> spawnPostionList = default;
    [Header("The Boss")]
    [SerializeField] private PumpkinBoss boss = default;
    [SerializeField] private GameObject bossGameObject = default;
    [SerializeField] private Collider2D bossHurtBox = default;

    [Header("Stages")]
    [SerializeField] private Stage stage = default;

    [Header("The Bossshield")]
    [SerializeField] private Shield bossshield = default;

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

    [Header("Dialogues while fighting")]
    [SerializeField] private Dialogue dialogue = default;

    private void Awake()
    {
        dialogue.npcName = "Sylandrel - The Pumpkin Wizard";
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
        playerTrigger.OnTriggerEnter += EnterBossArea;       //Subscribe to not start the Battle multiple Times
        boss.OnEnemyTakeDamage += BossTakesDamage;
        boss.OnEnemyDied += BossDied;
    }

    private void BossDied()
    {
        Debug.Log("The Boss died!");
        SoundManager.RequestSound(bossDiedSound);
        MusicManager.RequestMusic(endBattleMusic);
        DestroyAllEnemies();
        storeDefeated.RuntimeValue = true;                                                         // Stop spawning enemies
    }

    private void BossTakesDamage()
    {
        Debug.Log("Boss took DMG!");
        switch (stage)
        {
            case Stage.Stage_1:
                if (boss.GetPercentHealth() <= 70)
                {
                    StartNextStage();
                    CancelInvoke("SpawnEnemy");
                    SpawnMinions();
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
        if (bossGameObject.activeInHierarchy == true)
        {
            StartBattle();
            playerTrigger.OnTriggerEnter -= EnterBossArea;       //Unsubscribe to not start the Battle multiple Times
            triggerArea.SetActive(false);
        }
    }

    private void StartBattle()
    {
        Debug.Log("Battle has started!");
        boss.animator.SetTrigger("StartBattle");
        StartCoroutine(ActivateBossValuesCo());
        StartNextStage();
        InvokeRepeating("SpawnEnemy", 1.0f, spawnRate);
    }

    private void SpawnEnemy()
    {

        Enemy minion;

        if (stage == Stage.Stage_1)
        {
            Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
            minion = Instantiate(enemy1, spawnPoint, Quaternion.identity);
            SoundManager.RequestSound(spawnEnemysound);
        }
        else
        {
            Vector3 spawnPoint = spawnPostionList[Random.Range(0, spawnPostionList.Count)];
            minion = Instantiate(enemy2, spawnPoint, Quaternion.identity);
            SoundManager.RequestSound(spawnEnemysound);
        }
            spawnedEnemiesList.Add(minion);
    }

    private void MinionKilled()
    {
        spawnedMinionsCounter--;
        if (spawnedMinionsCounter <= 0)
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
        switch (stage)
        {
            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                bossshield.triggerShield();
                boss.HalfCooldownSpellTwo();
                Debug.Log("Shield should be active now!");
                break;
            case Stage.Stage_2:
                boss.HalfCooldownSpellTwo();
                stage = Stage.Stage_3;
                break;
        }
        Debug.Log("Next Stage has started " + stage);
    }

    private void EnhanceAttacks()
    {
        boss.HalfCooldown();
        boss.HalfCooldownSpellTwo();
    }

    private void SpawnMinions()
    {
        StartCoroutine(SpawnMinionsCo());
    }

    private IEnumerator ActivateBossValuesCo()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.RequestSound(startBattleSound);
        yield return new WaitForSeconds(3f);
        bossHurtBox.enabled = true;
        boss.GetComponent<PumpkinBoss>().enabled = true;
    }

    private IEnumerator SpawnMinionsCo()
    {
        dialogue.sentences[0] = "My Skeleton Champions will show you where you are supposed to be! ATTACK!";
        DialogueManager.RequestDialogue(dialogue);
        yield return new WaitForSeconds(2f);
        Enemy minion;
        for (int i = 0; i < minionsToSpawn; i++)
        {
            SoundManager.RequestSound(spawnEnemysound);
            Vector3 spawnPoint = spawnPostionList[i];
            minion = Instantiate(bossMinion, spawnPoint, Quaternion.identity);
            minion.isMinion = true;
            minion.OnMinionDied += MinionKilled;
            spawnedMinionsCounter++;
            yield return new WaitForSeconds(timeBetweenMinionSpawn);
        }
    }
}
