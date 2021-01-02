using UnityEngine;

public class HeartContainer : PickUp
{
    [SerializeField] private float amountToIncrease = default;

    protected override void PlayerPickUp(PlayerMovement player)
    {
        player.healthMeter.max += amountToIncrease;
        player.health = player.healthMeter.max;
        Destroy(this.gameObject);
    }
}
