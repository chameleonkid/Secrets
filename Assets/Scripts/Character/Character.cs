using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public State currentState { get; protected set; }
    public abstract float health { get; set; }

    public new Transform transform { get; private set; }
    public new Rigidbody2D rigidbody { get; private set; }
    protected Animator animator { get ; private set; }

    protected virtual void Awake() => GetCharacterComponents();

    public SoundManager soundManager;
    
    protected void GetCharacterComponents()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected void SetAnimatorXY(Vector2 direction)
    {
        direction.Normalize();
        if (direction != Vector2.zero)
        {
            // Need to round since animator expects integers
            direction.x = Mathf.Round(direction.x);
            direction.y = Mathf.Round(direction.y);

            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }

    protected void SetAnimatorXYSingleAxis(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            SetAnimatorXY(direction * Vector2.right);
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            SetAnimatorXY(direction * Vector2.up);
        }
    }

    //! A bit messy to have both a public health property and `TakeDamage`, but unsure how to address
    public virtual void TakeDamage(float damage) => health -= damage;

    public virtual void Knockback(Vector2 knockback, float duration)
    {
        if (this.gameObject.activeInHierarchy && currentState != State.stagger)
        {
            StartCoroutine(KnockbackCo(knockback, duration));
        }
    }

    protected IEnumerator KnockbackCo(Vector2 knockback, float duration)
    {
        currentState = State.stagger;
        rigidbody.AddForce(knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        rigidbody.velocity = Vector2.zero;
        currentState = State.idle;
    }

    public enum State {
        idle,
        walk,
        stagger,
        interact,
        attack,
        roundattack,
        lift,
        dead
    }
}
