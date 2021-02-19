using UnityEngine;

public class Move : State
{
    private ICanMove target;

    public Move(ICanMove target)
    {
        this.target = target;
    }
    
    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdate()
    {
        target.rigidbody.MovePosition((Vector2)target.transform.position
            + target.direction * target.moveSpeed * Time.deltaTime);
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}

public interface ICanMove
{
    Transform transform { get; }
    Rigidbody2D rigidbody { get; }
    Vector2 direction { get; }
    float moveSpeed { get; }
}
