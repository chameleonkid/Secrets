using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour , ISlow, IShrink,IGigantism
{
    public State currentState { get; protected set; }
    public abstract float health { get; set; }
    public bool isInvulnerable { get; set; }

    [SerializeField] protected SpriteRenderer _renderer = default;
    public new SpriteRenderer renderer => _renderer;

    public new Transform transform { get; private set; }
    public new Rigidbody2D rigidbody { get; private set; }
    public Animator animator { get; private set; }
    protected InvulnerabilityFrames iframes { get; private set; }
    [SerializeField] protected bool isShrinked;
    [Header("ParentSounds")]
    [SerializeField] protected AudioClip[] gotHitSound = default;
    [SerializeField] protected AudioClip[] attackSounds = default;
    [Header("CoolDowns")]
    [SerializeField] protected bool meeleCooldown = false;
    [SerializeField] protected bool spellCooldown = false;
    [SerializeField] protected bool spellTwoCooldown = false;
    [SerializeField] protected bool spellThreeCooldown = false;

    [Header("Inflictable Statuses")]
    [SerializeField] private Slow _slow = default;
    public Slow slow => _slow;
    [SerializeField] private Shrink _shrink = default;
    public Shrink shrink => _shrink;
    [SerializeField] private Gigantism _gigantism = default;
    public Gigantism gigantism => _gigantism;

    [Header("Dash Values")]
    [SerializeField] private float _maxDashDistance = 4;
    public float maxDashDistance => _maxDashDistance;
    [SerializeField] private float _dashDuration = 4;
    public float dashDuration => _dashDuration;
    [SerializeField] private bool _canDash = true;
    public bool canDash => _canDash;

    public float speedModifier { get; set; } = 1;

    private void Reset() => OnValidate(true);


    private void OnValidate() => OnValidate(false);
    private void OnValidate(bool overrideExisting)
    {
        if (overrideExisting || _renderer == null) _renderer = GetComponentInChildren<SpriteRenderer>();
        if (overrideExisting || _shrink == null) _shrink = GetComponentInChildren<Shrink>();
        if (overrideExisting || _gigantism == null) _gigantism = GetComponentInChildren<Gigantism>();
        //  if (overrideExisting || _dashless == null) _dashless = GetComponentInChildren<Dashless>();
        if (overrideExisting || _slow == null) _slow = GetComponentInChildren<Slow>();
    }







    protected virtual void Awake()
    {
        shrink?.Initialise(this);
        gigantism?.Initialise(this);
        //   dashless?.Initialise(this);
        slow?.Initialise(this);
        GetCharacterComponents();
    }

    protected void GetCharacterComponents()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        iframes = GetComponent<InvulnerabilityFrames>();
    }

    protected void SetAnimatorXY(Vector2 direction)
    {
        direction.Normalize();
        if (direction != Vector2.zero)
        {
            // Need to round since animator expects integers
            direction.x = Mathf.Round(direction.x);
            direction.y = Mathf.Round(direction.y);

            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
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
    public virtual void TakeDamage(float damage, bool isCritical)
    {
        if (!isInvulnerable)
        {
            health -= damage;
            DamagePopUpManager.RequestDamagePopUp(damage, isCritical, transform);
            SoundManager.RequestSound(gotHitSound.GetRandomElement());
            iframes?.TriggerInvulnerability();
        }
    }

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

    public enum State
    {
        idle,
        walk,
        stagger,
        interact,
        attack,
        roundattack,
        lift,
        dead,
        waiting
    }

    public void TeleportTowards(Vector2 destination, float maxDelta)
    {
        var difference = destination - (Vector2)transform.position;
        if (difference.magnitude > maxDelta)
        {
            difference.Normalize();
            transform.Translate(difference * maxDelta);
        }
        else
        {
            transform.Translate(difference);
        }
    }

    public void Dash(Character character,Vector2 forceDirection)
    {
        StartCoroutine(DashCo(character, forceDirection));
    }

    IEnumerator DashCo(Character character, Vector2 forceDirection)
    {
        for(int i = 0; i <= dashDuration; i++)
        {
            character.rigidbody.AddForce(forceDirection.normalized * 750f);
            character.rigidbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.01f);
        }

        
    }

}
