using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    [Header("Door Attributes")]
    [SerializeField] private bool isOpen = false;
    [SerializeField] private SpriteRenderer doorSpriteRenderer;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private Enemy[] enemies = default;


    private void OnEnable()
    {
        this.doorSpriteRenderer.sprite = closedSprite;
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].OnEnemyDied += CheckEnemies;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].OnEnemyDied -= CheckEnemies;
        }
    }

    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1)
            {
                return;
            }
        }
        OpenEnemyDoor();
    }


    public void OpenEnemyDoor()
    {
        this.doorSpriteRenderer.sprite = openedSprite;
        this.doorCollider.enabled = false;
    }
}
