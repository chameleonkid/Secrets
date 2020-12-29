using UnityEngine;

public class ItemPickUp : PickUp
{
    [SerializeField] private ItemOld item = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.myInventory.Add(item);
            Destroy(this.gameObject);
        }
    }
}
