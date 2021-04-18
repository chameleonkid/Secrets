using System.Collections;
using Schwer.States;
using UnityEngine;

public class SpawnTrap : ComponentTrigger<PlayerMovement>
{
    protected override bool? needOtherIsTrigger => true;

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
    [Header("This sound will be played when the trap will be activated:")]
    [SerializeField] private bool isActive = true;

    protected override void OnEnter(PlayerMovement player)
    {
        if (isActive)
        {
            StartCoroutine(StartSpawningCo());
            player.currentState = new Locked(player, lockTime);
        }
    }

    private IEnumerator StartSpawningCo()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
        isActive = false;
        SoundManager.RequestSound(trapSound);
        for (int i = 0; i < spawnCounter; i++)
        {
            Vector2 homePos = this.transform.position;
            var randomPosition = homePos + Random.insideUnitCircle * spawnRadius;
            var randomSpawn = Random.Range(0, thingToSpawn.Length);
            var spawn = Instantiate(thingToSpawn[randomSpawn], randomPosition, Quaternion.identity);  //this.transform.position needs to vary slightly for iteration
            yield return new WaitForSeconds(spawnDelay);
        }
        isActive = false;
    }
}
