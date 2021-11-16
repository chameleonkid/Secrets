﻿public class CoinBag : PickUp
{
   
    protected override void PlayerPickUp(PlayerMovement player)
    {
        player.inventory.coins = player.inventory.coins + 10;
        Destroy(this.gameObject);
        SoundManager.RequestSound(pickUpSound);
    }
}
