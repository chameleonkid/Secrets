using UnityEngine;

public class Heart : PowerUps
{
    public FloatValue playerHealth;
    public float amountToIncrease;
    public FloatValue HeartContainers;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("powerup") && other.isTrigger)
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
