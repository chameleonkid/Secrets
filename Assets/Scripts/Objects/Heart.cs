using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUps
{
    public FloatValue playerHealth;
    public float amountToIncrease;
    public FloatValue HeartContainers;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("powerup") && other.isTrigger)
        {
            playerHealth.RuntimeValue += amountToIncrease;
            if (playerHealth.RuntimeValue > HeartContainers.RuntimeValue * 2f)
            {
                playerHealth.RuntimeValue = HeartContainers.RuntimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }



}
