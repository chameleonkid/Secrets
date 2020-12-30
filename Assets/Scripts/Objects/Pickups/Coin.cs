using UnityEngine;

public class Coin : PickUp
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.myInventory.coins++;
            Destroy(this.gameObject);
        }
    }
}
