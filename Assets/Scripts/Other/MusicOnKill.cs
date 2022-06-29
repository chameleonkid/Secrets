using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnKill : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private AudioClip[] killMusic = default;


    private void OnEnable()
    {
        for (int i = 0; i < enemies.Length; i++)
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
            if (enemies[i].gameObject.activeInHierarchy)
            {
                return;
            }
        }
        MusicManager.RequestMusic(killMusic);
    }

}
