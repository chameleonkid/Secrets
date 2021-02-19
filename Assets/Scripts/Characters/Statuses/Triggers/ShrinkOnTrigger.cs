public class ShrinkOnTrigger : StatusOnTrigger<IShrink> {
    protected override void Trigger(IShrink target)
        => target.shrink.Trigger();
}
