using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storeOpen;

    [Header("Signals + Dialog")]
    public Signals raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    private Animator anim;

    [SerializeField] private AudioClip chestSound = default;
    [SerializeField] private AudioClip noInventorySpace = default;


    private void Start()
    {
        isOpen = storeOpen.RuntimeValue;
        anim = GetComponent<Animator>();
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }

        //dialogBox = GameObject.Find("DialogBox");                         Cant find it, since its disabled... is there another way?
        //dialogText = dialogBox.GetComponentInChildren<Text>();            That would be necessary to "Spawn" Chests
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (!isOpen && playerInventory.items.HasCapacity(contents) == true)
            {
                OpenChest();
            }
            else if(!isOpen && contents.unique && playerInventory.items.HasCapacity(contents) == false)
            {
                NoInventorySpace();
                SoundManager.RequestSound(noInventorySpace);
            }
            /*         
            else if (!isOpen && !contents.unique && playerInventory.items.HasCapacity(contents) == false && playerInventory.items.Contains(contents))  <--- how to check that??? And why is it working fine without that!?
            {
                playerInventory.items[contents]++;
            }
            */
            else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.description;

        playerInventory.currentItem = contents;
        playerInventory.items[contents]++;

        // raise the signal to animate

        raiseItem.Raise();
        // set the chest to opened
        isOpen = true;
        SoundManager.RequestSound(chestSound);
        //raise the context clue to off
        contextOff.Raise();
        anim.SetBool("opened", true);
        storeOpen.RuntimeValue = isOpen;
    }


    public void NoInventorySpace()
    {
        dialogBox.SetActive(true);
        dialogText.text = "There is no space left in your inventory";

    }



    public void ChestAlreadyOpen()
    {
        // Dialog off
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = true;
        }
        if (other.CompareTag("Player") && !isOpen)
        {
            contextOn.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = false;
            contextOff.Raise();
            dialogBox.SetActive(false);
        }
    }
}
