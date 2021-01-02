public class Coin : PickUp
{
    protected override void PlayerPickUp(PlayerMovement player)
    {
        player.inventory.coins++;
        Destroy(this.gameObject);
    }
}
