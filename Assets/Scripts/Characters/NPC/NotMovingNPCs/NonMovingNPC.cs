using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class NonMovingNPC : Character
{
    public override float health { get => 1; set { } }   //! Temp

    private Interactable interactable;
    [SerializeField] private bool lookUp;
    [SerializeField] private bool lookDown;
    [SerializeField] private bool lookLeft;
    [SerializeField] private bool lookRight;
    [SerializeField] private bool isDead;

    protected override void Awake()
    {
        base.Awake();
        interactable = GetComponent<Interactable>();
        if(lookUp)
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
        if (isDead)
        {
            animator.Play("Dead");
        }
    }
}
