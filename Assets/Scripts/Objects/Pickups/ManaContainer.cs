using UnityEngine;

public class ManaContainer : PickUp
{
    [SerializeField] private float amountToIncrease = default;

    protected override void PlayerPickUp(PlayerMovement player)
    {
        player.mana.max += amountToIncrease;
        player.mana.current = player.mana.max;
        Destroy(this.gameObject);
    }
}
