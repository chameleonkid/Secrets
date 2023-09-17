using System.Collections;
using Schwer.States;
using UnityEngine;

public abstract class Character : MonoBehaviour, ICanKnockback, ISlow, IShrink, IGigantism, IDashless, IPoison
{
    public StateEnum currentStateEnum { get; protected set; }
    public abstract float health { get; set; }
    public bool isInvulnerable { get; set; }
    public BoolValue cantMove;

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
    [SerializeField] protected AudioClip[] deathSounds = default;
    [SerializeField] protected AudioClip[] inRangeSounds = default;
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
    [SerializeField] private Dashless _dashless = default;
    public Dashless dashless => _dashless;
    [SerializeField] private Poison _poison = default;
    public Poison poison => _poison;

    [Header("Dash Values")]
    [SerializeField] private float _maxDashDistance = 4;
    public float maxDashDistance => _maxDashDistance;
    [SerializeField] private float _dashDuration = 4;
    public float dashDuration => _dashDuration;
    [SerializeField] private bool _canDash = true;
    public bool canDash { get; set; } = true;

    public float speedModifier { get; set; } = 1;

    private State _currentState;
    public State currentState {
        get => _currentState;
        set {
            if (value == null || value.GetType() != _currentState?.GetType()) {
                _currentState?.Exit();
                _currentState = value;
                _currentState?.Enter();
            }
        }
    }

    private void Reset() => OnValidate(true);
    private void OnValidate() => OnValidate(false);
    private void OnValidate(bool overrideExisting)
    {
        if (overrideExisting || _renderer == null) _renderer = GetComponentInChildren<SpriteRenderer>();
        if (overrideExisting || _shrink == null) _shrink = GetComponentInChildren<Shrink>();
        if (overrideExisting || _gigantism == null) _gigantism = GetComponentInChildren<Gigantism>();
        if (overrideExisting || _dashless == null) _dashless = GetComponentInChildren<Dashless>();
        if (overrideExisting || _slow == null) _slow = GetComponentInChildren<Slow>();
        if (overrideExisting || _poison == null) _poison = GetComponentInChildren<Poison>();
    }

    protected virtual void Awake()
    {
        GetCharacterComponents();

        shrink?.Initialise(this);
        gigantism?.Initialise(this);
        dashless?.Initialise(this);
        slow?.Initialise(this);
        poison?.Initialise(this);
    }

    protected void GetCharacterComponents()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        iframes = GetComponent<InvulnerabilityFrames>();
    }

    public void SetAnimatorXY(Vector2 direction)
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

    public void SetAnimatorXYSingleAxis(Vector2 direction)
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

    public Vector2 GetAnimatorXY() => new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));

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

    public virtual void TakeDamageNoArmor(float damage, bool isCritical)
    {
        if (!isInvulnerable)
        {
            health -= damage;
            DamagePopUpManager.RequestDamagePopUp(damage, isCritical, transform);
            SoundManager.RequestSound(gotHitSound.GetRandomElement());
            iframes?.TriggerInvulnerability();
        }
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

    public void Dash(Character character, Vector2 forceDirection, float dashForce) => StartCoroutine(DashCo(character, forceDirection, dashForce));

    private IEnumerator DashCo(Character character, Vector2 forceDirection, float dashForce)
    {
        for (int i = 0; i <= dashDuration; i++)
        {
            character.rigidbody.AddForce(forceDirection.normalized * dashForce);
            character.rigidbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public enum StateEnum
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

    public void RequestAttackSound()
    {
        SoundManager.RequestSound(attackSounds.GetRandomElement());
    }

    public void RequestGotHitSound()
    {
        SoundManager.RequestSound(gotHitSound.GetRandomElement());
    }
}
