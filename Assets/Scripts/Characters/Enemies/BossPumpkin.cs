using System.Collections;
using UnityEngine;

public class BossPumpkin : TurretEnemy
{
    [Header("AbilityValues Boss")]
    public GameObject[] projectileTwo;
    public bool canFireTwo = false;
    public float fireDelayTwo;
    [SerializeField] private float fireDelaySecondsTwo;

    protected override void Update()
    {
        if (currentState is Schwer.States.Knockback) return;

        base.Update();

        fireDelaySecondsTwo -= Time.deltaTime;
        if (fireDelaySecondsTwo <= 0)
        {
            canFireTwo = true;
            fireDelaySecondsTwo = fireDelayTwo;
        }
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentStateEnum = StateEnum.waiting;
    }

    protected override void InsideChaseRadiusUpdate()
    {
        base.InsideChaseRadiusUpdate();

        if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk && currentStateEnum != StateEnum.stagger)
        {
            if (canFireTwo)
            {
                currentStateEnum = StateEnum.attack;
                FireProjectileTwo();
            }
            currentStateEnum = StateEnum.idle;
        }
    }

    protected virtual void FireProjectileTwo()
    {
        StartCoroutine(FireTwoCo());
        canFireTwo = false;
        currentStateEnum = StateEnum.walk;
    }

    protected virtual IEnumerator FireTwoCo()
    {
        var originalMovespeed = this.moveSpeed;
        animator.Play("Attacking 2");
        SoundManager.RequestSound(attackSounds.GetRandomElement());
        yield return new WaitForSeconds(1f);
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0.5f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;
        var randomProjectile = Random.Range(0, projectileTwo.Length);
        var proj = Instantiate(projectileTwo[randomProjectile], target.transform.position, Quaternion.identity);
    }

    public void HalfCooldownSpellTwo()
    {
        fireDelayTwo = fireDelayTwo / 2;
    }
}
