using UnityEngine;

public class Heart : PickUp
{
    [SerializeField] private float amountToIncrease = default;

    protected override void PlayerPickUp(PlayerMovement player)
    {
        player.health += amountToIncrease;
        Destroy(this.gameObject);
    }
}
