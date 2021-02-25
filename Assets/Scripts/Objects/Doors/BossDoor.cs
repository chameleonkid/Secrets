using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [Header("Door Attributes")]
    [SerializeField] private SpriteRenderer doorSpriteRenderer = default;
    [SerializeField] private Sprite closedSprite = default;
    [SerializeField] private Sprite openedSprite = default;
    [SerializeField] private BoxCollider2D doorCollider = default;
    [SerializeField] private EnemyBoss[] bosses = default;
    [Header("Saves")]
    [SerializeField] private bool isDefeated;
    [SerializeField] private BoolValue storeDefeated;


    private void Awake()
    {

        isDefeated = storeDefeated.RuntimeValue;
        if (isDefeated)
        {
            OpenEnemyDoor();
        }
    }

        private void OnEnable()
    {
        this.doorSpriteRenderer.sprite = closedSprite;
        for (int i = 0; i < bosses.Length; i++)
        {
            bosses[i].OnBossDied += CheckBossDead;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < bosses.Length; i++)
        {
            bosses[i].OnBossDied -= CheckBossDead;
        }
    }

    public void CheckBossDead()
    {
        for (int i = 0; i < bosses.Length; i++)
        {
            if (bosses[i].gameObject.activeInHierarchy)
            {
                return;
            }
        }
        OpenEnemyDoor();
    }


    public void OpenEnemyDoor()
    {
        this.doorSpriteRenderer.enabled = false;
        this.doorCollider.enabled = false;
    }
}

