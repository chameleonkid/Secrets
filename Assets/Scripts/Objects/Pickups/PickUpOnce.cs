using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpOnce : ItemPickUp
{
    [SerializeField] private BoolValue pickupSave;

    public void Awake()
    {
        if(pickupSave.RuntimeValue == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    protected override void PlayerPickUp(PlayerMovement player)
    {
        if (player.inventory.items.HasCapacity(item))
        {
            player.inventory.items[item]++;
            pickupSave.RuntimeValue = true;
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Couldnt pick " + item + " up, since there was no space left");
        }
    }


}
