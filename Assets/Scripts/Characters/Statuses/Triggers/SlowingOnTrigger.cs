public class SlowingOnTrigger : StatusOnTrigger<ISlow>
{

    protected override void Trigger(ISlow target)
        => target.slow.Trigger();
}
