public class Coin : PickUp
{
    protected override void PlayerPickUp(PlayerMovement player)
    {
        player.myInventory.coins++;
        Destroy(this.gameObject);
    }
}
