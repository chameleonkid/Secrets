using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrap : MonoBehaviour
{
    [Header("Choose random things to spawn of this:")]
    [SerializeField] private GameObject[] thingToSpawn;
    [Header("How many Randoms should be spawned?:")]
    [SerializeField] private int spawnCounter;
    [Header("How much delay between spawns?:")]
    [SerializeField] private float spawnDelay;
    [Header("In which radius of the trap will be spawned?:")]
    [SerializeField] private float spawnRadius;
    [Header("How long will the player be stuck?:")]
    [SerializeField] private float lockTime;
    [Header("This sound will be played when the trap will be activated:")]
    [SerializeField] private AudioClip trapSound;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>() && other.isTrigger)
        {
            StartSpawning();
            other.GetComponent<PlayerMovement>().LockMovement(lockTime);
        }
    }

    protected virtual void StartSpawning()
    {
        StartCoroutine(StartSpawningCo());
    }


    protected virtual IEnumerator StartSpawningCo()
    {
        SoundManager.RequestSound(trapSound);
        for(int i = 0; i < spawnCounter; i++)
        {
            Vector2 homePos = this.transform.position;
            var randomPosition = homePos + Random.insideUnitCircle * spawnRadius;
            var randomSpawn = Random.Range(0, thingToSpawn.Length);
            var spawn = Instantiate(thingToSpawn[randomSpawn], randomPosition, Quaternion.identity);  //this.transform.position needs to vary slightly for iteration
            yield return new WaitForSeconds(spawnDelay);
        }

    }


}
