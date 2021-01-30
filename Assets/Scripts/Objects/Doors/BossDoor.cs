using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour


{

    [Header("Door Attributes")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private BoxCollider2D doorCollider;
    [Header("Which Boss?")]
    [SerializeField] private Enemy boss;
    [Header("Check if Boss was defeated already")]
    [SerializeField] private BoolValue isBossDead = default;
    // Start is called before the first frame update


    private void OnEnable()
    {
        boss.OnEnemyDied += CheckBossKill;
    }

    private void OnDisable()
    {
        boss.OnEnemyDied -= CheckBossKill;
    }

    private void Awake()
    {
        CheckBossKill();
    }



    public void OpenBossDoor()
    {
        this.doorSpriteRenderer.sprite = openedSprite;
        this.doorCollider.enabled = false;
    }

    public void CloseBossDoor()
    {
        this.doorSpriteRenderer.sprite = closedSprite;
        this.doorCollider.enabled = true;
    }

    public void CheckBossKill()
    {
        StartCoroutine(CheckBossKillCo());
    }

    IEnumerator CheckBossKillCo()
    {
        yield return new WaitForSeconds(1.5f);
        if (isBossDead.RuntimeValue)
        {
            OpenBossDoor();
        }
        else
        {
            CloseBossDoor();
        }
    }
}
