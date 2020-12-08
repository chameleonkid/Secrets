using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (playerInventory && thisItem)
            {
                playerInventory.Add(thisItem);
            }
            Destroy(this.gameObject);
        }
    }
}
