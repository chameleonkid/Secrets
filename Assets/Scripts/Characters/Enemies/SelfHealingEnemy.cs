using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHealingEnemy : Enemy
{
    [SerializeField] private float SelfHealAmount;
    [SerializeField] private float healDelay = 1;
    [SerializeField] private float OriginalHealDelay = 1;
    [SerializeField] private float healDelaySeconds;
    [SerializeField] private bool  canHeal = false;

    protected override void Awake()
    {
        base.Awake();
        OriginalHealDelay = healDelay;
    }


    protected override void FixedUpdate()
    {

        healDelaySeconds -= Time.deltaTime;
        if (healDelaySeconds <= 0)
        {
            canHeal = true;
            healDelaySeconds = healDelay;
        }


        var percentHeal = maxHealth.value / 100f;
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius && this.health > (percentHeal * 10))
        {
            InsideChaseRadiusUpdate();
        }

        else if (distance <= chaseRadius && distance > attackRadius && this.health <= (percentHeal * 10))
        {
            Flee();
        }
        else if (distance > chaseRadius)
        {
            OutsideChaseRadiusUpdate();
        }

        if (this.health <= (percentHeal * 75))
        {
            if(this.health <= (percentHeal * 50))
            {
                healDelay = (OriginalHealDelay / 4);
            }
            else
            {
                healDelay = OriginalHealDelay;
            }
            SelfHeal();
        }
    }

    private void SelfHeal()
    {
        StartCoroutine(SelfHealCo());
        canHeal = false;
    }

    IEnumerator SelfHealCo()
    {
        if(canHeal == true)
        {
            if (this.health < this.maxHealth.value)
            {
                if (this.health + SelfHealAmount > this.maxHealth.value)
                {
                    this.health = this.maxHealth.value;
                }
                this.health += SelfHealAmount;
                DamagePopUpManager.RequestHealPopUp(SelfHealAmount, this.transform);
            }
            yield return new WaitForSeconds(0);
        }
        canHeal = false;
    }

    
}
