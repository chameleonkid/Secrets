public abstract class State
{
    public abstract void Enter();
    public virtual void Update() {}
    public virtual void FixedUpdate() {}
    public abstract void Exit();
}
