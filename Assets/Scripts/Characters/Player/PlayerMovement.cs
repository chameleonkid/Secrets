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

    [SerializeField] private bool notStaggeredOrLifting = default;

    [SerializeField] private Animator effectAnimator = default;

    [SerializeField] private XPSystem levelSystem = default;
    [SerializeField] private float _speed = default;
    private float speed => (Input.GetButton("Run")) ? _speed * 1.5f : _speed;
    [SerializeField] private float originalSpeed;

    private Vector3 change;
    public Vector2 direction => change;
    public float moveSpeed => speed * speedModifier;

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
        uiAttackButton.GetComponent<Button>().onClick.AddListener(MeleeAttack);
        uiSpellButton.GetComponent<Button>().onClick.AddListener(UISpellAttack);
        uiSpellTwoButton.GetComponent<Button>().onClick.AddListener(UISpellAttackTwo);
        uiSpellThreeButton.GetComponent<Button>().onClick.AddListener(UISpellAttackThree);
        uiLampButton.GetComponent<Button>().onClick.AddListener(ToggleLamp);
    }

    private void Update()
    {
        if (Time.timeScale <= 0) return;

        // ################################################# Change Inputtype to Joystick on IOS ################################################
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        //  change.x = joystick.Horizontal;
        //  change.y = joystick.Vertical;

        HandleState();

        currentState?.Update();

        animator.SetBool("isRunning", Input.GetButton("Run") && change != Vector3.zero);

        notStaggeredOrLifting = (currentStateEnum != StateEnum.stagger && currentStateEnum != StateEnum.lift);

        if (Input.GetButtonDown("Attack"))
        {
            MeleeAttack();
        }

        if (Input.GetButton("SpellCast"))
        {
            SpellAttack(inventory.currentSpellbook);
        }
        if (Input.GetButton("SpellCast2"))  //Getbutton in GetButtonDown für die nicht dauerhafte Abfrage
        {
            SpellAttack(inventory.currentSpellbookTwo);
        }
        if (Input.GetButton("SpellCast3"))  //Getbutton in GetButtonDown für die nicht dauerhafte Abfrage
        {
            SpellAttack(inventory.currentSpellbookThree);
        }
        if (Input.GetButtonDown("Lamp"))
        {
            ToggleLamp();
        }
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();

        if (!(currentState is Schwer.States.Knockback))
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void HandleState()
    {
        // Full state machine logic should be here
        if (currentState == null)
        {
            currentState = new Move(this);
        }
    }

    public bool IsCriticalHit() => (inventory.totalCritChance > 0 && Random.Range(0, 99) <= inventory.totalCritChance);

    // #################################### Casual Attack ####################################
    private IEnumerator AttackCo()
    {
        var currentWeapon = inventory.currentWeapon;

        if (inventory.currentWeapon.weaponType == InventoryWeapon.WeaponType.Bow)
        {
            if (arrow != null && inventory.items[arrow] > 0)
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
                yield return new WaitForSeconds(currentWeapon.swingTime);
                meeleCooldown = false;
                SoundManager.RequestSound(meleeCooldownSound);
            }
        }
        else
        {
            OnAttackTriggered?.Invoke();
            meeleCooldown = true;
            // This part is not working properly in BUILD
            hitBoxColliders[0].points = currentWeapon.upHitboxPolygon;
            hitBoxColliders[1].points = currentWeapon.downHitboxPolygon;
            hitBoxColliders[2].points = currentWeapon.rightHitboxPolygon;
            hitBoxColliders[3].points = currentWeapon.leftHitboxPolygon;
            //! ^ The order of the hitboxes colliders cannot be safely determined by index,
            //    as the order is arbitrarily assigned via Inspector.

            weaponSkinChanger.newSprite = currentWeapon.weaponSkin;

            if (currentWeapon.weaponSkin != oldWeaponSkin)
            {
                weaponSkinChanger.ResetRenderer();
            }
            oldWeaponSkin = currentWeapon.weaponSkin;

            var isCritical = IsCriticalHit();
            for (int i = 0; i < directionalAttacks.Length; i++)
            {
                directionalAttacks[i].damage = Random.Range(inventory.currentWeapon.minDamage, inventory.currentWeapon.maxDamage + 1);
                directionalAttacks[i].isCritical = isCritical;
            }

            SoundManager.RequestSound(attackSounds.GetRandomElement());

            animator.SetBool("Attacking", true);
            currentStateEnum = StateEnum.attack;
            yield return null;
            animator.SetBool("Attacking", false);
            yield return new WaitForSeconds(0.3f);

            if (currentStateEnum != StateEnum.interact)
            {
                currentStateEnum = StateEnum.walk;
            }

            yield return new WaitForSeconds(currentWeapon.swingTime);
            meeleCooldown = false;
            SoundManager.RequestSound(meleeCooldownSound);
        }
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
            if (!(currentState is Locked))
            {
                currentState = new Locked(this, "receiveItem");
                receivedItemSprite.sprite = inventory.currentItem.sprite;
            }
            else
            {
                currentState = null;
                receivedItemSprite.sprite = null;
                inventory.currentItem = null;
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

    public void MeleeAttack()
    {
        if (currentStateEnum != StateEnum.attack && inventory.currentWeapon != null && meeleCooldown == false)
        {
            StartCoroutine(AttackCo());
        }
    }

    public void UISpellAttack() => SpellAttack(inventory.currentSpellbook);

    public void UISpellAttackTwo() => SpellAttack(inventory.currentSpellbookTwo);

    public void UISpellAttackThree() => SpellAttack(inventory.currentSpellbookThree);

    public void ToggleLamp()
    {
        if (inventory.currentLamp && lumen.current > 0)
        {
            lamp.enabled = !lamp.enabled;
            OnLampTriggered?.Invoke();
        }
    }

    public void LockMovement(float seconds) => StartCoroutine(LockCo(seconds));

    private IEnumerator LockCo(float seconds)
    {
        this._speed = 0;
        yield return new WaitForSeconds(seconds);
        this._speed = this.originalSpeed;
    }

    public void SpellAttack(InventorySpellbook spellBook)  // Does this need to be public?
    {
        if (spellBook != null && mana.current >= spellBook.manaCost && notStaggeredOrLifting && currentStateEnum != StateEnum.attack && !spellBook.onCooldown)
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
}
