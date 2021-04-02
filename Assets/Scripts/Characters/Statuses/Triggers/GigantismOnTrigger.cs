public class GigantismOnTrigger : StatusOnTrigger<IGigantism>
{
    protected override void Trigger(IGigantism target)
        => target.gigantism.Trigger();
}
