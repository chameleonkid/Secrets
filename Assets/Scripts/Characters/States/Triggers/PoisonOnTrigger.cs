public class PoisonOnTrigger : StatusOnTrigger<IPoison>
{

    protected override void Trigger(IPoison target)
        => target.poison.Trigger();
}
