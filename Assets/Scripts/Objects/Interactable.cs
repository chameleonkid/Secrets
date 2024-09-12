public class Interactable : ComponentTrigger<PlayerMovement>
{
    protected override bool? needOtherIsTrigger => true;

    public Signals contextOn;
    public Signals contextOff;

    public bool playerInRange => player != null;
    protected PlayerMovement player;

    protected override void OnEnter(PlayerMovement player)
    {
        contextOn.Raise();
        this.player = player;
    }

    protected override void OnExit(PlayerMovement player)
    {
        contextOff.Raise();
        this.player = null;
    }
}
