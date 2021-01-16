using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMeleeEnemy : TurretEnemy
{
    protected override void InsideChaseRadiusUpdate()
    {
        animator.SetBool("isInRange", true);
        
        if (canAttack)
        {

            currentState = State.attack;
            MeleeAttack();
        }

        currentState = State.idle;
        
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentState = State.idle;
        animator.SetBool("isInRange", false);
    }

    protected virtual void MeleeAttack()
    {
        StartCoroutine(MeleeAttackCo());
        canAttack = false;
        currentState = State.walk;
    }

    protected virtual IEnumerator MeleeAttackCo()
    {
        animator.SetBool("isAttacking", true);
        Debug.Log("PLANT IS ATTACKING!!!!");
        // SoundManager.RequestSound(attackSounds.GetRandomElement());
        yield return new WaitForSeconds(1f);              //This would equal the "CastTime"
        animator.SetBool("isAttacking", false);
    }


}
