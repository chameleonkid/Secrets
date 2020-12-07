using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUps
{

    public PlayerInventory PlayerInventory;

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("powerup") && other.isTrigger)
        {
            PlayerInventory.coins += 1;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }

}
