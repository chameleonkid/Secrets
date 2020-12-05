using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_collectable : PowerUps
{

    public PlayerInventory PlayerInventory;
    public InventoryItem arrow;

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("powerup") && other.isTrigger)
        {
            PlayerInventory.Add(arrow);
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }



}
