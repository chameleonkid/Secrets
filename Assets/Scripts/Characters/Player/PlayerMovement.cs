using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Schwer.States;

public class PlayerMovement : Character, ICanMove
{
    [Header("TestUIInputs")]
    [SerializeField] private Button uiAttackButton = default;
    [SerializeField] private Button uiSpellButton = default;
    [SerializeField] private Button uiSpellTwoButton = default;
    [SerializeField] private Button uiSpellThreeButton = default;
    [SerializeField] private Button uiLampButton = default;
    [SerializeField] private Joystick joystick = default;

    [SerializeField] private Animator effectAnimator = default;

    [SerializeField] private XPSystem levelSystem = default;
    [SerializeField] private float _speed = default;
    private float speed => input.run ? _speed * 1.5f : _speed;
    [SerializeField] private float originalSpeed;

    private PlayerInput input;
    public Vector2 direction => input.direction;
    public float moveSpeed => speed * speedModifier;

    private PlayerInput uiInput;

    [SerializeField] private ConstrainedFloat _lumen = default;
    public ConstrainedFloat lumen => _lumen;
    [SerializeField] private ConstrainedFloat _mana = default;
    public ConstrainedFloat mana => _mana;
    [SerializeField] private ConstrainedFloat _health = default;
    public ConstrainedFloat healthMeter => _health;
    public override float health
    {
        get => _health.current;
        set
        {
            _health.current = value;
            if (_health.current <= 0)
            {
                currentState = new PlayerDead(this);
            }
        }
    }

    public VectorValue startingPosition;

    public Inventory inventory;
    [SerializeField] private Item arrow = default;

    public SpriteRenderer receivedItemSprite;

    [Header("Hitboxes")]
    [SerializeField] private DamageOnTrigger[] directionalAttacks = default;
    [SerializeField] private DamageOnTrigger roundAttack = default;
    [SerializeField] private PolygonCollider2D[] hitBoxColliders = default;

    [Header("Projectiles")]
    [SerializeField] private float arrowSpeed = 1;
    public GameObject projectile; //arrows and so on

    [Header("Sound FX")]
    [SerializeField] private AudioClip levelUpSound = default;
    [SerializeField] private AudioClip meleeCooldownSound = default;
    [SerializeField] private AudioClip Spell0CooldownSound = default;

    [Header("Lamp")]
    [SerializeField] private LampLight lamp = default;

    public SpriteRenderer thingSprite;

    [Header("WeaponSkins")]

    [SerializeField] private SpriteSkinRPC weaponSkinChanger = default;
    private Texture2D oldWeaponSkin = default;

    public event Action OnAttackTriggered;
    public event Action OnSpellTriggered;
    public event Action OnSpellTwoTriggered;
    public event Action OnSpellThreeTriggered;
    public event Action OnLampTriggered;

    private void OnEnable() => levelSystem.OnLevelChanged += LevelUpPlayer;
    private void OnDisable() => levelSystem.OnLevelChanged -= LevelUpPlayer;

    private void LevelUpPlayer()
    {
        _health.max = _health.max + 10;
        _mana.max = _mana.max + 10;
        _health.current = _health.max;
        _mana.current = _mana.max;
        SoundManager.RequestSound(levelUpSound);
        if (effectAnimator)
        {
            effectAnimator.Play("LevelUp");
        }
    }

    private void Start()
    {
        SetAnimatorXY(Vector2.down);
        currentState = new Move(this);
        transform.position = startingPosition.value;
        originalSpeed = speed;

        // This is for Using UI-Buttons
        uiAttackButton.onClick.AddListener(InputAttack);
        uiSpellButton.onClick.AddListener(InputSpell1);
        uiSpellTwoButton.onClick.AddListener(InputSpell2);
        uiSpellThreeButton.onClick.AddListener(InputSpell3);
        uiLampButton.onClick.AddListener(InputLamp);
    }

    private void Update()
    {
        if (Time.timeScale <= 0) return;

        HandleInput();
        HandleState();
        Debug.Log(currentState);
        currentState?.Update();

        animator.SetBool("isRunning", input.run && input.direction != Vector2.zero); //!

        uiInput.ClearBools();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();

        if (!(currentState is Schwer.States.Knockback))
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        input.direction.x = Input.GetAxisRaw("Horizontal");
        input.direction.y = Input.GetAxisRaw("Vertical");

        input.run = Input.GetButton("Run");

        input.attack = Input.GetButtonDown("Attack");
        input.lamp = Input.GetButtonDown("Lamp");

        // GetButton in GetButtonDown für die nicht dauerhafte Abfrage
        input.spellCast1 = Input.GetButton("SpellCast");
        input.spellCast2 = Input.GetButton("SpellCast2");
        input.spellCast3 = Input.GetButton("SpellCast3");
    }

    private void HandleState()
    {
        // Full state machine logic should be here
        if (currentState == null) currentState = new Move(this);

        if (input.attack || uiInput.attack) Attack();

        if (!(currentState is Schwer.States.Knockback))
        {
            if (input.spellCast1 || uiInput.spellCast1) SpellAttack(inventory.currentSpellbook);
            if (input.spellCast2 || uiInput.spellCast2) SpellAttack(inventory.currentSpellbookTwo);
            if (input.spellCast3 || uiInput.spellCast3) SpellAttack(inventory.currentSpellbookThree);
        }

        if (input.lamp || uiInput.lamp) ToggleLamp();
    }

    public bool IsCriticalHit() => (inventory.totalCritChance > 0 && Random.Range(0, 99) <= inventory.totalCritChance);

    // #################################### Casual Attack ####################################
    private void Attack()
    {
        var currentWeapon = inventory.currentWeapon;

        if (currentStateEnum != StateEnum.attack && currentWeapon != null && !meeleCooldown)
        {
            if (currentWeapon.weaponType == InventoryWeapon.WeaponType.Bow)
            {
                if (arrow != null && inventory.items[arrow] > 0)
                {
                    StartCoroutine(BowAttackCo(currentWeapon));
                }
            }
            else
            {
                StartCoroutine(MeleeAttackCo(currentWeapon));
            }
        }
    }

    private IEnumerator MeleeAttackCo(InventoryWeapon weapon)
    {
        OnAttackTriggered?.Invoke();
        meeleCooldown = true;
        // This part is not working properly in BUILD
        hitBoxColliders[0].points = weapon.upHitboxPolygon;
        hitBoxColliders[1].points = weapon.downHitboxPolygon;
        hitBoxColliders[2].points = weapon.rightHitboxPolygon;
        hitBoxColliders[3].points = weapon.leftHitboxPolygon;
        //! ^ The order of the hitboxes colliders cannot be safely determined by index,
        //    as the order is arbitrarily assigned via Inspector.

        weaponSkinChanger.newSprite = weapon.weaponSkin;

        if (weapon.weaponSkin != oldWeaponSkin)
        {
            weaponSkinChanger.ResetRenderer();
        }
        oldWeaponSkin = weapon.weaponSkin;

        var isCritical = IsCriticalHit();
        for (int i = 0; i < directionalAttacks.Length; i++)
        {
            directionalAttacks[i].damage = Random.Range(inventory.currentWeapon.minDamage, inventory.currentWeapon.maxDamage + 1);
            directionalAttacks[i].isCritical = isCritical;
        }

        SoundManager.RequestSound(attackSounds.GetRandomElement());

        // currentState = new PlayerMeleeAttack(this, 0.3f);
        animator.SetBool("Attacking", true);
        currentStateEnum = StateEnum.attack;
        yield return null;
        animator.SetBool("Attacking", false);

        yield return new WaitForSeconds(0.3f + weapon.swingTime);
        meeleCooldown = false;
        SoundManager.RequestSound(meleeCooldownSound);
    }

    private IEnumerator BowAttackCo(InventoryWeapon weapon)
    {
        OnAttackTriggered?.Invoke();
        meeleCooldown = true;
        inventory.items[arrow]--;
        currentStateEnum = StateEnum.attack;
        animator.SetBool("isShooting", true);

        var proj = CreateProjectile(projectile);
        var damage = Random.Range(inventory.currentWeapon.minDamage, inventory.currentWeapon.maxDamage + 1);
        proj.OverrideSpeed(arrowSpeed);
        proj.OverrideDamage(damage, IsCriticalHit());

        yield return new WaitForSeconds(0.3f);

        if (currentStateEnum != StateEnum.interact)
        {
            currentStateEnum = StateEnum.walk;
        }
        animator.SetBool("isShooting", false);
        yield return new WaitForSeconds(weapon.swingTime);
        meeleCooldown = false;
        SoundManager.RequestSound(meleeCooldownSound);
    }

    // ############################# Roundattack ################################################
    private IEnumerator RoundAttackCo()
    {
        roundAttack.damage = Random.Range(inventory.currentWeapon.minDamage, inventory.currentWeapon.maxDamage + 1);
        roundAttack.isCritical = IsCriticalHit();
        //! Is this missing a sound request?
        animator.SetBool("RoundAttacking", true);
        currentStateEnum = StateEnum.roundattack;
        yield return null;  //! This allows a round attack to be executed every other frame when the input is held, causing mana to drain very quickly
        animator.SetBool("RoundAttacking", false);
        currentStateEnum = StateEnum.walk;

        mana.current -= 1;
    }

    private Projectile CreateProjectile(GameObject prefab)
    {
        var position = new Vector2(transform.position.x, transform.position.y + 0.5f);      // Set projectile higher since transform is at player's pivot point (feet).
        var direction = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var projectile = Projectile.Instantiate(prefab, position, direction, Projectile.CalculateRotation(direction), "enemy");
        return projectile;
    }

    // ############################## Using the SpellBook /Spellcasting #########################################
    private IEnumerator SpellAttackCo(InventorySpellbook spellBook)
    {
        switch (spellBook)
        {
            default:
                break;
            case InstantiationSpellbook instantiationSpellbook:
                var damage = Random.Range(inventory.totalMinSpellDamage, inventory.totalMaxSpellDamage + 1);
                CreateProjectile(instantiationSpellbook.prefab);
                animator.SetBool("isCasting", true);
                break;
            case UnityEventSpell eventSpell:
                eventSpell.Use();
                animator.SetBool("isCasting", true);
                break;
            case DashSpell dashSpell:
                dashSpell.Dash(this);
                break;
        }

        spellBook.onCooldown = true;
        yield return new WaitForSeconds(0.05f);
        if (currentStateEnum != StateEnum.interact)
        {
            currentStateEnum = StateEnum.walk;
        }
        animator.SetBool("isCasting", false);
        yield return new WaitForSeconds(spellBook.coolDown);
        SoundManager.RequestSound(Spell0CooldownSound);
        spellBook.onCooldown = false;
    }

    //#################################### Item Found RAISE IT! #######################################

    public void RaiseItem()
    {
        if (inventory.currentItem != null)
        {
            if (currentState is Locked)
            {
                currentState = null;
                receivedItemSprite.sprite = null;
                inventory.currentItem = null;
            }
            else
            {
                currentState = new Locked(this, "receiveItem");
                receivedItemSprite.sprite = inventory.currentItem.sprite;
            }
        }
    }

    public override void TakeDamage(float damage, bool isCritical)
    {
        if (!isInvulnerable)
        {
            inventory.CalcDefense();
            var finalDamage = damage - inventory.totalDefense;
            if (finalDamage > 0)
            {
                health -= finalDamage;
                DamagePopUpManager.RequestDamagePopUp(finalDamage, isCritical, transform);
                SoundManager.RequestSound(gotHitSound.GetRandomElement());
                iframes?.TriggerInvulnerability();
            }
            else
            {
                DamagePopUpManager.RequestDamagePopUp(0, isCritical, transform);
            }
            //    Debug.Log(finalDamage + " damage after defense calculation.");
        }
    }

    // ############################################# Refactor ####################################################################################
    // ################################### Functions for UI Input or Controller ##################################################################
    // ###########################################################################################################################################

    public void ToggleLamp()
    {
        if (inventory.currentLamp && lumen.current > 0)
        {
            lamp.enabled = !lamp.enabled;
            OnLampTriggered?.Invoke();
        }
    }

    public void SpellAttack(InventorySpellbook spellBook)  // Does this need to be public?
    {
        if (spellBook != null && mana.current >= spellBook.manaCost && currentStateEnum != StateEnum.attack && !spellBook.onCooldown)
        {
            mana.current -= spellBook.manaCost;
            StartCoroutine(SpellAttackCo(spellBook));

            if (spellBook == inventory.currentSpellbook)
            {
                OnSpellTriggered?.Invoke();
            }
            else if (spellBook == inventory.currentSpellbookTwo)
            {
                OnSpellTwoTriggered?.Invoke();
            }
            else if (spellBook == inventory.currentSpellbookThree)
            {
                OnSpellThreeTriggered?.Invoke();
            }
        }
    }

    #region UI Controls
    public void InputAttack() => uiInput.attack = true;
    public void InputSpell1() => uiInput.spellCast1 = true;
    public void InputSpell2() => uiInput.spellCast2 = true;
    public void InputSpell3() => uiInput.spellCast3 = true;
    public void InputLamp() => uiInput.lamp = true;
    #endregion
}
