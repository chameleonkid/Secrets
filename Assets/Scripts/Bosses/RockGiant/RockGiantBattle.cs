using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGiantBattle : MonoBehaviour
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
    [Header("The Boss")]
    [SerializeField] private RockGiantBoss boss = default;
    [SerializeField] private GameObject bossGameObject = default;
    [SerializeField] private Collider2D bossHurtBox = default;
    [Header("TriggerArea")]
    [SerializeField] private GameObject leaveBlock = default;


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
            bossGameObject.SetActive(false);
            triggerArea.SetActive(false);
        }
        else
        {
            bossGameObject.SetActive(true);
            stage = Stage.WaitingToStart;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTrigger.OnTriggerEnter += EnterBossArea;       //Subscribe to not start the Battle multiple Times
        boss.OnEnemyTakeDamage += BossTakesDamage;
        boss.OnEnemyDied += BossDied;
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

    private void BossDied()
    {
        Debug.Log("One Boss died!");
        SoundManager.RequestSound(bossDiedSound);
        leaveBlock.SetActive(false);
        MusicManager.RequestMusic(endBattleMusic);
        storeDefeated.RuntimeValue = true;

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
        StartCoroutine(ActivateBossValuesCo());
        StartNextStage();
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
        boss.HalfCooldown();
        boss.HalfCooldownSpellTwo();
    }

    private IEnumerator ActivateBossValuesCo()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.RequestSound(startBattleSound);
        yield return new WaitForSeconds(3f);
        bossHurtBox.enabled = true;
        boss.GetComponent<RockGiantBoss>().enabled = true;
    }

}



