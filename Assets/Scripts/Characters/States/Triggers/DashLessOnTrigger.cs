public class DashLessOnTrigger : StatusOnTrigger<IDashless>
{
    protected override void Trigger(IDashless target)
    {
        target.dashless.Trigger();
    }
}
