using UnityEngine;

public class HealthCrystal : PickUp
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
        player.healthMeter.max += amountToIncrease;
        player.health = player.healthMeter.max;
        isPickedUp = true;
        SoundManager.RequestSound(pickUpSound);
        Destroy(this.gameObject);
    }
}
