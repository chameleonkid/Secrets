using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public Signals healthSignal;
    public FloatValue HeartContainers;


    public void Use(int amountToIncrease)
    {
        playerHealth.RuntimeValue += amountToIncrease;
        if (playerHealth.RuntimeValue > HeartContainers.RuntimeValue * 2f)
        {
            playerHealth.RuntimeValue = HeartContainers.RuntimeValue * 2f;
        }
        healthSignal.Raise();


    }

}