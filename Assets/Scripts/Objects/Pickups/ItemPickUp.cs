using UnityEngine;

public class ItemPickUp : PickUp
{
    [SerializeField] private Item item = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.myInventory.items[item]++;
            Destroy(this.gameObject);
        }
    }
}
