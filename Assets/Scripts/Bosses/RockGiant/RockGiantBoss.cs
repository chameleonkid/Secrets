using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGiantBoss : TurretEnemy
{


    [Header("Boulder ability")]
    [SerializeField] private float canThrowBoulderCD;
    public float canThrowBoulderTimer;
    public bool canThrowBoulder;
    [SerializeField] private float boulderYPosition;

    [Header("Earthquake ability")]
    [SerializeField] private float canEarthquakeCD;
    public float canEarthquakeTimer;
    public bool canEarthquake;
    [Header("Choose random things to spawn of this:")]
    [SerializeField] private GameObject[] thingToSpawn;
    [Header("How many Randoms should be spawned?:")]
    [SerializeField] private int spawnCounter;
    [Header("How much delay between spawns?:")]
    [SerializeField] private float spawnDelay;
    [Header("In which radius of the trap will be spawned?:")]
    [SerializeField] private float spawnRadius;
    [Header("Broken Earth underneath the food of Rock Giant")]
    [SerializeField] private GameObject brokenEarth;
    [Header("Correction of the Y-Axis of brokenEarth")]
    [SerializeField] private float brokenEarthYPosition;
    [Header("Correction of the Y-Axis of brokenEarth")]
    [SerializeField] private float brokenEarthDestroyTimer;

    [Header("Boss Attack Sounds")]
    [SerializeField] private AudioClip[] attackSound;
    [SerializeField] private AudioClip earthQuakeSound;

    [Header("Starting Dialog")]
    [SerializeField] private Dialogue dialogue = default;




    protected override void FixedUpdate()
    {
        // if (currentState is Schwer.States.Knockback) return;

        //base.FixedUpdate();

        //###################### Throw Boulder Ability ######################################
        canThrowBoulderCD -= Time.deltaTime;
        if (canThrowBoulderCD <= 0)
        {
            canThrowBoulder = true;
            canThrowBoulderCD = canThrowBoulderTimer;
        }
        if (canThrowBoulder == true)
        {
            animator.SetTrigger("ThrowBoulder");
            canThrowBoulder = false;
            //throwAttack(); -------> Set in the animation!!!
        }
        //#########################  Earthquake Ability  #######################################
        canEarthquakeCD -= Time.deltaTime;
        if (canEarthquakeCD <= 0)
        {
            canEarthquake = true;
            canEarthquakeCD = canEarthquakeTimer;
        }
        if (canEarthquake == true)
        {
            animator.SetTrigger("Earthquake");
            canEarthquake = false;
            //Earthquake(); -------> Set in the animation!!!
        }


}

   


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentStateEnum = StateEnum.waiting;
    }

    protected override void InsideChaseRadiusUpdate()
    {

    }

    private void ThrowAttack()
    {
        StartCoroutine(StoneThrowCo());
    }

    private void InstantiateBrokenEarth()
    {
        SoundManager.RequestSound(earthQuakeSound);
        var brokenEarthPosition = new Vector3(this.transform.position.x, this.transform.position.y + brokenEarthYPosition);
        GameObject brokenEarthPrefab = Instantiate(brokenEarth, brokenEarthPosition, Quaternion.identity);
        Destroy(brokenEarthPrefab, brokenEarthDestroyTimer);
    }

    private void Earthquake()
    {
        StartCoroutine(StartEarthQuakeCo());
    }


    protected virtual IEnumerator StoneThrowCo()
    {
        var originalMovespeed = this.moveSpeed;
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;


        for (int i = 0; i <= amountOfProjectiles - 1; i++)
        {
            var projectilePosition = new Vector3(transform.position.x, transform.position.y + boulderYPosition);
            var difference = target.transform.position - projectilePosition;
            Projectile.Instantiate(projectile, projectilePosition, difference, Quaternion.identity, "Player");
            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
    }



    public void PlayAttackSound()
    {
        SoundManager.RequestSound(attackSound.GetRandomElement());
    }

    private IEnumerator StartEarthQuakeCo()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
        for (int i = 0; i < spawnCounter; i++)
        {
            Vector2 homePos = this.transform.position;
            var randomPosition = homePos + Random.insideUnitCircle * spawnRadius;
            var randomSpawn = Random.Range(0, thingToSpawn.Length);
            var spawn = Instantiate(thingToSpawn[randomSpawn], randomPosition, Quaternion.identity);  //this.transform.position needs to vary slightly for iteration
            yield return new WaitForSeconds(spawnDelay);
        }
    }


    protected override void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
      //      Destroy(effect, deathEffectDelay); keep the StoneStatue alive forever
        }
    }

    void RequestDialogue()
    {
        dialogue.lines[0].text = "WHO IS DISTURBING ALVAREZ! WHAT??? YOU? I WILL SMASH YOU LIKE AN ANT!";
        DialogueManager.RequestDialogue(dialogue);
    }


}
