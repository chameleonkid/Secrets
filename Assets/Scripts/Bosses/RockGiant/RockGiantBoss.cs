using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGiantBoss : TurretEnemy
{
    [Header("AbilityValues Boss")]

    [Header("Boss Attack Sounds")]
    [SerializeField] private AudioClip[] attackSound;
    [SerializeField] private AudioClip earthQuakeSound;

    protected override void FixedUpdate()
    {
        if (currentState is Schwer.States.Knockback) return;

        base.FixedUpdate();

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
        if (currentStateEnum == StateEnum.idle || currentStateEnum == StateEnum.walk && currentStateEnum != StateEnum.stagger)
        {
            if (canAttack)
            {
                currentStateEnum = StateEnum.attack;
                animator.SetTrigger("isThrowing");
                //throwAttack(); -------> Set in the animation!!!
            }
            currentStateEnum = StateEnum.walk;

        }
    }

    private void throwAttack()
    {
        StartCoroutine(StoneThrowCo());
    }
       

    protected virtual IEnumerator StoneThrowCo()
    {
        var originalMovespeed = this.moveSpeed;
        //animator.Play("Attacking");
        //SoundManager.RequestSound(attackSounds.GetRandomElement());
        this.moveSpeed = 0;
        yield return new WaitForSeconds(0f);              //This would equal the "CastTime"
        this.moveSpeed = originalMovespeed;


        for (int i = 0; i <= amountOfProjectiles - 1; i++)
        {
            var difference = target.transform.position - transform.position;
            Projectile.Instantiate(projectile, transform.position, difference, Quaternion.identity, "Player");
            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
        animator.SetBool("Attacking", false);

    }



    public void PlayAttackSound()
    {
        SoundManager.RequestSound(attackSound.GetRandomElement());
    }

}
