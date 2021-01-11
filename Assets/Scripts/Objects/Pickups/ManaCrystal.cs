using UnityEngine;

public class ManaCrystal: PickUp
{

    [SerializeField] private float amountToIncrease = default;
    [SerializeField] private BoolValue storePickedUp = default;
    [SerializeField] private bool isPickedUp { get => storePickedUp.RuntimeValue; set => storePickedUp.RuntimeValue = value; }

    private void Start()
    {
        isPickedUp = storePickedUp.RuntimeValue;
        if (isPickedUp)
        {
            Destroy(this.gameObject);
        }
    }

    protected override void PlayerPickUp(PlayerMovement player)
    {
        player.mana.max += amountToIncrease;
        player.mana.current = player.mana.max;
        isPickedUp = true;
        Destroy(this.gameObject);
    }
}
