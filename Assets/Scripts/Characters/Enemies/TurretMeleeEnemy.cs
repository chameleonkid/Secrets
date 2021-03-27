using System.Collections;
using UnityEngine;

public class TurretMeleeEnemy : TurretEnemy
{
    protected override void InsideChaseRadiusUpdate()
    {
        animator.SetBool("isInRange", true);

        if (canAttack)
        {
            currentStateEnum = StateEnum.attack;
            MeleeAttack();
        }

        currentStateEnum = StateEnum.idle;
    }

    protected override void OutsideChaseRadiusUpdate()
    {
        currentStateEnum = StateEnum.idle;
        animator.SetBool("isInRange", false);
    }

    protected virtual void MeleeAttack()
    {
        StartCoroutine(MeleeAttackCo());
        canAttack = false;
        currentStateEnum = StateEnum.walk;
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
