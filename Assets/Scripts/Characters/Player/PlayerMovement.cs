using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class PlayerMovement : Character
{
    [Header("Appearance")]
    [SerializeField] private SpriteSkinRPC bodySkin = default;
    [SerializeField] private SpriteSkinRPC armorSkin = default;
    [SerializeField] private SpriteSkinRPC hairStyle = default;
    [SerializeField] private SpriteRenderer hairRenderer = default;
    [SerializeField] private SpriteSkinRPC eyesSkin = default;
    private Color hairColor;
    [SerializeField] private CharacterAppearance characterAppearance = default;


    [Header("TestInputs")]
    [SerializeField] private Button uiAttackButton = default;
    [SerializeField] private Button uiSpellButton = default;
    [SerializeField] private Button uiSpellTwoButton = default;
    [SerializeField] private Button uiLampButton = default;
    [SerializeField] private Joystick joystick = default;

    [SerializeField] private bool notStaggeredOrLifting = default;

    [SerializeField] private Animator effectAnimator = default;

    [SerializeField] private XPSystem levelSystem = default;
    [SerializeField] private float _speed = default;
    private float speed => (Input.GetButton("Run")) ? _speed * 2 : _speed;
    [SerializeField] private float originalSpeed;

    private Vector3 change;

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
                StartCoroutine(DeathCo());
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
    public event Action OnLampTriggered;

    private void OnEnable() => levelSystem.OnLevelChanged += LevelUpPlayer;
    private void OnDisable() => levelSystem.OnLevelChanged -= LevelUpPlayer;

    protected override void Awake()
    {
    GetCharacterComponents();
        SetAppearance();
    }


    private void LevelUpPlayer()
    {
        _health.max = _health.max + 10;
        _mana.max = _mana.max + 10;
        _health.current = _health.max;
        _mana.current = _mana.max;
        currentState = State.idle;
        SoundManager.RequestSound(levelUpSound);
        if (effectAnimator)
        {
            effectAnimator.Play("LevelUp");
        }
    }

    private void Start()
    {
        SetAnimatorXY(Vector2.down);
        currentState = State.walk;
        transform.position = startingPosition.value;
        originalSpeed = speed;


        // This is for Using UI-Buttons
        uiAttackButton.GetComponent<Button>().onClick.AddListener(MeleeAttack);
        uiSpellButton.GetComponent<Button>().onClick.AddListener(SpellAttack);
        uiSpellTwoButton.GetComponent<Button>().onClick.AddListener(SpellAttackTwo);
        uiLampButton.GetComponent<Button>().onClick.AddListener(ToggleLamp);

    }

    private AudioClip GetLevelUpSound() => levelUpSound;

    private void Update()
    {
        // Is the player in an interaction?
        if (currentState == State.interact)
        {
            // Debug.Log("helpmeout");
            return;
        }

        // ################################################# Change Inputtype to Joystick on IOS ################################################
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        //  change.x = joystick.Horizontal;
        //  change.y = joystick.Vertical;

        SetAnimatorXY(change);

        animator.SetBool("isRunning", Input.GetButton("Run") && change != Vector3.zero);

        notStaggeredOrLifting = (currentState != State.stagger && currentState != State.lift);

        if (Input.GetButtonDown("Attack"))
        {
            MeleeAttack();
        }

        if (Input.GetButton("SpellCast"))
        {
            SpellAttack();
        }

        if (Input.GetButtonDown("Lamp"))
        {
            ToggleLamp();
        }

        //########################################################################### Round Attack if Mana > 0 ##################################################################################
        if (Input.GetButton("RoundAttack"))  //Getbutton in GetButtonDown für die nicht dauerhafte Abfrage
        {
            SpellAttackTwo();
        }
        //########################################################################### Bow Shooting with new Inventory ##################################################################################



        //##############################################################################################################################################################

        if (Input.GetButtonUp("UseItem"))
        {
            animator.SetBool("isShooting", false);
        }

        animator.SetBool("isHurt", (currentState == State.stagger));
        animator.SetBool("Moving", (change != Vector3.zero));
    }

    private void FixedUpdate()
    {
        if (currentState == State.walk || currentState == State.idle || currentState == State.lift)
        {
            rigidbody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
        }

        if (currentState != State.stagger)
        {
            rigidbody.velocity = Vector2.zero;
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
                currentState = State.attack;
                animator.SetBool("isShooting", true);
                CreateProjectile(projectile, arrowSpeed, Random.Range(inventory.currentWeapon.minDamage, inventory.currentWeapon.maxDamage + 1));
                yield return new WaitForSeconds(0.3f);

                if (currentState != State.interact)
                {
                    currentState = State.walk;
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
            currentState = State.attack;
            yield return null;
            animator.SetBool("Attacking", false);
            yield return new WaitForSeconds(0.3f);

            if (currentState != State.interact)
            {
                currentState = State.walk;
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
        currentState = State.roundattack;
        yield return null;  //! This allows a round attack to be executed every other frame when the input is held, causing mana to drain very quickly
        animator.SetBool("RoundAttacking", false);
        currentState = State.walk;

        mana.current -= 1;
    }

    private void CreateProjectile(GameObject projectilePrefab, float projectileSpeed, float projectileDamage)
    {
        var position = new Vector2(transform.position.x, transform.position.y + 0.5f); // Pfeil höher setzen
        var direction = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var proj = Instantiate(projectilePrefab, position, Projectile.CalculateRotation(direction)).GetComponent<Projectile>();
        proj.rigidbody.velocity = direction.normalized * projectileSpeed; // This makes the object move
        var hitbox = proj.GetComponent<DamageOnTrigger>();
        if (hitbox)
        {
            hitbox.damage = projectileDamage;    //replace defaultvalue with the value given from the makespell()/playervalue
            hitbox.isCritical = IsCriticalHit();  // gets written into Derived class
        }
    }

    // ############################## Using the SpellBook /Spellcasting #########################################
    private IEnumerator SpellAttackCo()
    {
        animator.SetBool("isCasting", true); // Set to cast Animation
        currentState = State.attack;
        MakeSpell();
        OnSpellTriggered?.Invoke();
        spellCooldown = true;
        yield return new WaitForSeconds(0.05f);
        if (currentState != State.interact)
        {
            currentState = State.walk;
        }
        animator.SetBool("isCasting", false);
        yield return new WaitForSeconds(inventory.currentSpellbook.coolDown);
        SoundManager.RequestSound(Spell0CooldownSound);
        spellCooldown = false;
    }

    private IEnumerator SpellAttackTwoCo()
    {
        animator.SetBool("isCasting", true); // Set to cast Animation
        currentState = State.attack;
        MakeSpellTwo();
        OnSpellTwoTriggered?.Invoke();
        spellTwoCooldown = true;
        yield return new WaitForSeconds(0.05f);
        if (currentState != State.interact)
        {
            currentState = State.walk;
        }
        animator.SetBool("isCasting", false);
        yield return new WaitForSeconds(inventory.currentSpellbookTwo.coolDown);
        SoundManager.RequestSound(Spell0CooldownSound);
        spellTwoCooldown = false;
    }

    //################### instantiate spell when casted ###############################
    private void MakeSpell()
    {
        var prefab = inventory.currentSpellbook.prefab;
        // var speed = inventory.currentSpellbook.speed;
        var speed = prefab.GetComponent<Projectile>().projectileSpeed;
        CreateProjectile(prefab, speed, Random.Range(inventory.totalMinSpellDamage, inventory.totalMaxSpellDamage + 1));
        mana.current -= inventory.currentSpellbook.manaCosts;
    }

    private void MakeSpellTwo()
    {
        var prefab = inventory.currentSpellbookTwo.prefab;
        // var speed = inventory.currentSpellbook.speed;
        var speed = prefab.GetComponent<Projectile>().projectileSpeed;
        CreateProjectile(prefab, speed, Random.Range(inventory.totalMinSpellDamage, inventory.totalMaxSpellDamage + 1));
        mana.current -= inventory.currentSpellbookTwo.manaCosts;
    }

    //#################################### Item Found RAISE IT! #######################################

    public void RaiseItem()
    {
        if (inventory.currentItem != null)
        {
            if (currentState != State.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = State.interact;
                receivedItemSprite.sprite = inventory.currentItem.sprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = State.idle;
                receivedItemSprite.sprite = null;
                inventory.currentItem = null;
            }
        }
    }

    // #################################### LIFT Item ######################################
    /* 
       public void LiftItem()
       {
           if (playerInventory.currentItem != null)
           {
               if (currentState != State.lift)
               {             
                   animator.SetBool("isCarrying", true);
                   currentState = State.lift;
                   Debug.Log("State = Lift");
                   thingSprite.sprite = myInventory.currentItem.itemImage;
               }
               else
               {   
                   animator.SetBool("isCarrying", false);
                   currentState = State.idle;
                   Debug.Log("State = idle");
                   thingSprite.sprite = null;

                   myInventory.currentItem = null;
               }
           }
       }
    */

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

    // ########################### Getting hit and die ##############################################

    public override void Knockback(Vector2 knockback, float duration)
    {
        if (currentState != State.stagger && this.gameObject.activeInHierarchy)
        {
            StartCoroutine(KnockbackCo(knockback, duration));
        }
    }

    //##################### Death animation and screen ##############################

    private IEnumerator DeathCo()
    {
        currentState = State.dead;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("DeathMenu");
    }

    //############################################# Refcator #####################################################################################
    //################################### Functions for UI Input or Controller ###################################################################
    //############################################################################################################################################
    //############################################################################################################################################

    public void MeleeAttack()
    {
        if (currentState != State.attack && inventory.currentWeapon != null && meeleCooldown == false)
        {
            StartCoroutine(AttackCo());
        }
    }

    public void SpellAttack()
    {
        if (inventory.currentSpellbook && mana.current > 0 && notStaggeredOrLifting && currentState != State.attack && spellCooldown == false)
        {
            StartCoroutine(SpellAttackCo());
        }
    }

    public void SpellAttackTwo()
    {
        if (inventory.currentSpellbookTwo && mana.current > 0 && notStaggeredOrLifting && currentState != State.attack && spellTwoCooldown == false)
        {
            StartCoroutine(SpellAttackTwoCo());
        }
    }

    public void ToggleLamp()
    {
        if (inventory.currentLamp && lumen.current > 0)
        {
            lamp.enabled = !lamp.enabled;
            OnLampTriggered?.Invoke();
        }
    }


    public void LockMovement(float seconds)
    {
        StartCoroutine(LockCo(seconds));
    }

    private IEnumerator LockCo(float seconds)
    {
        this._speed = 0;
        yield return new WaitForSeconds(seconds);
        this._speed = this.originalSpeed;
    }

    private void SetAppearance()
    {
        if (characterAppearance.bodyStyle)
        {
            bodySkin.newSprite = characterAppearance.bodyStyle;
            bodySkin.ResetRenderer();
        }
        if (characterAppearance.armorStyle)
        {
            armorSkin.newSprite = characterAppearance.armorStyle;
            armorSkin.ResetRenderer();
        }
        if (characterAppearance.hairStyle)
        {
            hairStyle.newSprite = characterAppearance.hairStyle;
            hairRenderer.color = characterAppearance.hairColor;
            hairStyle.ResetRenderer();
        }
        if (characterAppearance.eyeColor)
        {
            eyesSkin.newSprite = characterAppearance.eyeColor;
            eyesSkin.ResetRenderer();
        }
    }

}



//############################################################################################################################################
//############################################################################################################################################
