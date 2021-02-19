using UnityEngine;

public class Move : State
{
    private ICanMove target;

    public Move(ICanMove target) => this.target = target;

    public override void Update()
    {
        target.SetAnimatorXY(target.direction);
        target.animator.SetBool("Moving", (target.direction != Vector2.zero));
    }

    public override void FixedUpdate()
    {
        target.rigidbody.MovePosition((Vector2)target.transform.position
            + target.direction * target.moveSpeed * Time.deltaTime);
    }

    public override void Exit()
    {
        target.animator.SetBool("Moving", false);
    }
}

public interface ICanMove
{
    Transform transform { get; }
    Rigidbody2D rigidbody { get; }
    Animator animator { get; }

    Vector2 direction { get; }
    float moveSpeed { get; }

    void SetAnimatorXY(Vector2 direction);
}
