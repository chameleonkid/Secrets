using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimator : MonoBehaviour
{
    [SerializeField] private GameObject spawnAnimation = default;
    [SerializeField] private GameObject enemy = default;
    [SerializeField] private float spawnDuration = default;

    public void Awake()
    {
        StartSpawnAnimation();
    }

    public void StartSpawnAnimation()
    {
        StartWarning();
        Invoke("SpawnEnemy", this.spawnDuration);
    }

    public void StartWarning()
    {
        if (spawnAnimation)
        {
            spawnAnimation.SetActive(true);
        }
    }

    public void StopWarning()
    {
        if (spawnAnimation)
        {
            spawnAnimation.SetActive(false);
        }
    }

    public void SpawnEnemy()
    {
        StopWarning();
        enemy.SetActive(true);
    }



}
