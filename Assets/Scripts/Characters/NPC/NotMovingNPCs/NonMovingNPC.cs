using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class NonMovingNPC : Character
{
    public override float health { get => 1; set { } }   //! Temp

    private Interactable interactable;
    [SerializeField] private bool sitting = false;
    [SerializeField] private bool lookUp;
    [SerializeField] private bool lookDown;
    [SerializeField] private bool lookLeft;
    [SerializeField] private bool lookRight;
    [SerializeField] private bool isDead;
    [SerializeField] private bool isHappy;
    [SerializeField] private bool isNodding;
    [SerializeField] private bool isCasting;

    public enum CharacterAnimationState
    {
        Idle,
        Sitting,
        Dead,
        Happy,
        Nodding,
        Casting
    }

    [SerializeField] private CharacterAnimationState animationState = CharacterAnimationState.Idle;


    protected override void Awake()
    {
        base.Awake();

        animator.speed = Random.Range(0.8f, 1.2f); //makes the idle npcs not blink at the same time
        interactable = GetComponent<Interactable>();
 
        if (lookUp)
        {
            animator.SetFloat("moveY", 1);
        }
        if (lookDown)
        {
            animator.SetFloat("moveY", -1);
        }
        if (lookLeft)
        {
            animator.SetFloat("moveX", -1);
        }
        if (lookRight)
        {
            animator.SetFloat("moveX", 1);
        }
        switch (animationState)
        {
            case CharacterAnimationState.Sitting:
                animator.SetBool("isSitting", true);
                break;
            case CharacterAnimationState.Dead:
                animator.Play("Dead");
                break;
            case CharacterAnimationState.Happy:
                animator.Play("Happy");
                break;
            case CharacterAnimationState.Nodding:
                animator.Play("Nodding");
                break;
            case CharacterAnimationState.Casting:
                animator.Play("Casting");
                break;
            default:
                break;
        }
    }
}
