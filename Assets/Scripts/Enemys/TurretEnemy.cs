using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyLog
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public float projectileSpeed;


    private void Update()
    {
        

        fireDelaySeconds -= Time.deltaTime;
        if(fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }



    public override void CheckDistance()
    {


        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
              //  Debug.Log("In TurretEnemy in Radius");
                if (canFire)
                {
                  //  Debug.Log("FIRE!!!!");
                    Vector3 tempVector = target.transform.position - transform.position; //Distance between us
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<BaseProjectile>().Launch(tempVector.normalized * projectileSpeed ); //Speed of projectile
                    canFire = false;
                    ChangeState(EnemyState.walk);
                    anim.SetBool("WakeUp", true);
                }
            }

        }
        if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("WakeUp", false);
            //   ChangeState(EnemyState.idle);
        }



    }

}
