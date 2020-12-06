using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("powerup") && other.isTrigger)
        {
            addItemToInventory();
            Destroy(this.gameObject);
        }
    }

    void addItemToInventory()
    {
        if (playerInventory && thisItem)
        {
            if (playerInventory.myInventory.Contains(thisItem))
            {
                thisItem.numberHeld++;
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld++;
            }
        }
    }
}
