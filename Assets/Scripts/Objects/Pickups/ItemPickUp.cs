using UnityEngine;

public class ItemPickUp : PickUp
{
    [SerializeField] private Item item = default;

    protected override void PlayerPickUp(PlayerMovement player)
    {
        if (player.myInventory.items.HasCapacity(item))
        {
            player.myInventory.items[item]++;
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Couldnt pick " + item + " up, since there was no space left");
        }
    }
}
