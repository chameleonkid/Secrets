using UnityEngine;

public class ItemPickUp : PickUp
{
    [SerializeField] protected Item item = default;

    protected override void PlayerPickUp(PlayerMovement player)
    {
        if (player.inventory.items.HasCapacity(item))
        {
            player.inventory.items[item]++;
            if(pickUpSound)
            {
                SoundManager.RequestSound(pickUpSound);
            }
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Couldnt pick " + item + " up, since there was no space left");
        }
    }
}
