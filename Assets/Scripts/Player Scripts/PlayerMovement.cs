using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerState
{
    walk,
    attack,
    interact,
    roundattack,
    stagger,
    idle,
    lift,
    dead
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signals playerHealthSignal;
    public VectorValue startingPosition;
    private bool isRunning;

    public Inventory playerInventory; //Old Inventory

    public PlayerInventory myInventory; // New Inventory

    public SpriteRenderer receivedItemSprite;
    public Signals playerHit;
    public GameObject projectile; //arrows and so on
    public Signals ArrowUsed;
    // public Item Bow;
    public InventoryItem Bow;
    public InventoryWeapon Sword;

    public Color FlashColor;
    public float flashDuration;
    public int numberOfFlasches;
    public Collider2D triggerCollider;
    public Color regularPlayerColor;
    public Color regularHairColor;
    public Color regularArmorColor;
    public SpriteRenderer playerSprite;
    public SpriteRenderer armorSprite;
    public SpriteRenderer hairSprite;

    //############### LIFT-TEST      ##############
    //  public GameObject thing;
    public SpriteRenderer thingSprite;
    //############### LIFT-TEST-ENDE ##############




    // Start is called before the first frame update
    void Start()
    {
        regularPlayerColor = playerSprite.color;
        regularHairColor = hairSprite.color;
        regularArmorColor = armorSprite.color;
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = startingPosition.initialValue;
    } 

    // Update is called once per frame
    void Update()
    {


        if(currentState != PlayerState.stagger)
        {
            myRigidbody.velocity = Vector2.zero;
        }
        // Is the player in an interaction?
        if(currentState == PlayerState.interact)
        {
        //    Debug.Log("helpmeout");
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Run") && !isRunning)  //Getbutton in GetButtonDown für die nicht dauerhafte Abfrage
        {
             speed = (speed * 2);
             isRunning = true;

            animator.SetBool("isRunning", true);

        }
        if (Input.GetButtonUp("Run") && isRunning)  //Getbutton in GetButtonDown für die nicht dauerhafte Abfrage
        {
            speed = (speed / 2);
            isRunning = false;
      
            animator.SetBool("isRunning", false);

        }
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.lift && myInventory.currentWeapon != null)
        { 
            StartCoroutine(AttackCO());
            //Debug.Log("Attack");
        }
        if (Input.GetButton("RoundAttack") && currentState != PlayerState.roundattack && currentState != PlayerState.stagger && currentState != PlayerState.lift && myInventory.currentWeapon != null)  //Getbutton in GetButtonDown für die nicht dauerhafte Abfrage
        {
            //Debug.Log("RoundAttack");
            StartCoroutine(RoundAttackCO());
        }
        //########################################################################### Bow Shooting with new Inventory ##################################################################################
        if (Input.GetButton("UseItem") && currentState != PlayerState.roundattack && currentState != PlayerState.stagger && currentState != PlayerState.lift && currentState != PlayerState.attack)
        {

            if (myInventory.myInventory.Find(x => x.itemName.Contains("Arrow")) && myInventory.myInventory.Find(x => x.itemName.Contains("Arrow")).numberHeld > 0 && myInventory.currentBow)
            {
                myInventory.myInventory.Find(x => x.itemName.Contains("Arrow")).numberHeld--;
                ArrowUsed.Raise();
                StartCoroutine(SecondAttackCo());
            }
        }
        if(Input.GetButtonUp("UseItem"))
        {
            animator.SetBool("isShooting", false);
        }
        //############################################################# Gets Hurt #################################################
        if (currentState == PlayerState.stagger)
        {
            animator.SetBool("isHurt", true);
        }
        else
        {
            animator.SetBool("isHurt", false);
        }

        //################################# Trying to drop things ################################################################
        if (Input.GetButtonDown("Lift") && currentState == PlayerState.lift)
        {
            LiftItem();
            Debug.Log("Item Dropped!");

        }
        //################################# Trying to drop things  END ############################################################

    }

    private void FixedUpdate()
    {

        if (currentState == PlayerState.walk || currentState == PlayerState.idle || currentState == PlayerState.lift)
        {
            UpdateAnimationAndMove();
        }

    }


    // #################################### Casual Attack ####################################
    private IEnumerator AttackCO()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

    }

    //#################################### Item Found RAISE IT! #######################################

    public void RaiseItem()
    {
        if (myInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                // receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;   Old Inventory
                receivedItemSprite.sprite = myInventory.currentItem.itemImage;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                myInventory.currentItem = null;
            }
        }
    }


    // #################################### LIFT Item ######################################
   
    public void LiftItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.lift)
            {             
                animator.SetBool("isCarrying", true);
                currentState = PlayerState.lift;
                Debug.Log("PlayerState = Lift");
                thingSprite.sprite = myInventory.currentItem.itemImage;
            }
            else
            {   
                animator.SetBool("isCarrying", false);
                currentState = PlayerState.idle;
                Debug.Log("Playerstate = idle");
                thingSprite.sprite = null;
                //        playerInventory.RemoveItem(playerInventory.currentItem);
                myInventory.currentItem = null;
                
            }
        }

    }
  


    // ############################# Roundattack ################################################
    private IEnumerator RoundAttackCO()
    {
        animator.SetBool("RoundAttacking", true);
        currentState = PlayerState.roundattack;
        yield return null;
        animator.SetBool("RoundAttacking", false);
       // yield return new WaitForSeconds(.0f); //Frames für stehenbleiben
        currentState = PlayerState.walk;
    }

    // ########################### Animation for Moving #########################################

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
        MoveCharacter();                    // Move Character
        change.x = Mathf.Round(change.x);   // Round the movement for deciding where to hit if walking diagonally
        change.y = Mathf.Round(change.y);   // Round the movement for deciding where to hit if walking diagonally
        animator.SetFloat("MoveX", change.x);
        animator.SetFloat("MoveY", change.y);
        animator.SetBool("Moving", true);
        }
    else
         {
         animator.SetBool("Moving", false);
         }
    }

    //############################### Move Character #############################################

    void MoveCharacter()
    {
        if (currentState != PlayerState.dead)       // Nur bewegen wenn nicht tot
        {

            change.Normalize();
            myRigidbody.MovePosition(
                transform.position + change * speed * Time.deltaTime
                );

        }
    }

    // ########################### Getting hit and die ##############################################

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            StartCoroutine(DeathCo());
        }
    }

    //############################# Knockback ################################################

    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    // ############################## Using the Item / Shooting the Bow #########################################
    private IEnumerator SecondAttackCo()
    {
        //animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
       // animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

    }

    //################### instantiate arrow when shot ###############################
    private void MakeArrow()
    {
        animator.SetBool("isShooting", true);
        Vector2 arrowHeight = new Vector2(transform.position.x, transform.position.y + 0.5f); // Pfeil höher setzen
        Vector2 temp = new Vector2(animator.GetFloat("MoveX"),animator.GetFloat("MoveY"));
        arrow arrow = Instantiate(projectile, arrowHeight, Quaternion.identity).GetComponent<arrow>();
        arrow.Setup(temp, ChooseArrowDirection());
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("MoveY"),animator.GetFloat("MoveX"))* Mathf.Rad2Deg;
        return new Vector3(0,0,temp);
    }

    //##################### Death animation and screen ##############################

    private IEnumerator DeathCo()
    {
        currentState = PlayerState.dead;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("DeathMenu");
    }



    //##################### Invulnerability frame ##############################

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlasches)
        {
            playerSprite.color = FlashColor;
            armorSprite.color = FlashColor;
            hairSprite.color = FlashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularPlayerColor;
            armorSprite.color = regularArmorColor;
            hairSprite.color = regularHairColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }

}
