using System.Collections;
using UnityEngine;

public class PumpkinBoss : TurretEnemy
{
    [Header("AbilityValues Boss")]
    public GameObject[] projectileTwo;
    public bool canFireTwo = false;
    public float fireDelayTwo;
    [SerializeField] private float fireDelaySecondsTwo;

    [Header("Boss Attack Sounds")]
    [SerializeField] private AudioClip attack1Sound;
    [SerializeField] private AudioClip attack2Sound;

    protected override void Update()
    {
    //################# CD for Projectile 1 ############################
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            animator.SetTrigger("CanFireOrb");
            fireDelaySeconds = fireDelay;
        }

        //################# CD for Projectile 2 ############################
        fireDelaySecondsTwo -= Time.deltaTime;
        if (fireDelaySecondsTwo <= 0)
        {
            animator.SetTrigger("CanSpawnPumpkinBomb");
            fireDelaySecondsTwo = fireDelayTwo;
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentStateEnum = StateEnum.waiting;
    }

    protected override void InsideChaseRadiusUpdate()
    {
 
    }

    protected override void FireProjectile()
    {
        StartCoroutine(FireCo());
    }

    protected override IEnumerator FireCo()
    {
        yield return new WaitForSeconds(0f);              //This would equal the "CastTime"
        
        for (int i = 0; i <= amountOfProjectiles - 1; i++)
        {
            var difference = target.transform.position - transform.position;
            GameObject orb = Projectile.Instantiate(projectile, transform.position, difference, Quaternion.identity, "Player").gameObject;
            yield return new WaitForSeconds(timeBetweenProjectiles);
            Destroy(orb, projectileDestructionTimer);
        }
  
    }


    protected virtual void FireProjectileTwo()
    {
        StartCoroutine(FireTwoCo());
    }

    protected virtual IEnumerator FireTwoCo()
    {
        yield return new WaitForSeconds(0f);            
        var randomProjectile = Random.Range(0, projectileTwo.Length);
        var proj = Instantiate(projectileTwo[randomProjectile], target.transform.position, Quaternion.identity);
    }

    public void HalfCooldownSpellTwo()
    {
        fireDelayTwo = fireDelayTwo / 3;
    }

    public void PlayAttack1Sound()
    {
        SoundManager.RequestSound(attack1Sound);
    }

    public void PlayAttack2Sound()
    {
        SoundManager.RequestSound(attack2Sound);
    }
}
