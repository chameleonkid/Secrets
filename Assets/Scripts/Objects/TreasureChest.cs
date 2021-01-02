using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    [SerializeField] private Item contents = default;
    [SerializeField] private BoolValue storeOpen = default;
    private bool isOpen { get => storeOpen.RuntimeValue; set => storeOpen.RuntimeValue = value; }

    [Header("Signals & Dialog")]
    [SerializeField] private Signals raiseItem = default;
    [SerializeField] private GameObject dialogBox = default;
    [SerializeField] private Text dialogText = default;

    [Header("Sound FX")]
    [SerializeField] private AudioClip chestSound = default;
    [SerializeField] private AudioClip noInventorySpace = default;

    private Animator anim;
    private PlayerMovement player;

    private void Start()
    {
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
            if (!isOpen)
            {
                if (player.inventory.items.HasCapacity(contents))
                {
                    OpenChest();
                }
                else
                {
                    NoInventorySpace();
                }
            }
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

        player.inventory.currentItem = contents;
        player.inventory.items[contents]++;

        // raise the signal to animate
        raiseItem.Raise();
        // set the chest to opened
        isOpen = true;
        SoundManager.RequestSound(chestSound);
        //raise the context clue to off
        contextOff.Raise();
        anim.SetBool("opened", true);
    }

    public void NoInventorySpace()
    {
        SoundManager.RequestSound(noInventorySpace);
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
            player = other.GetComponent<PlayerMovement>();

            if (!isOpen)
            {
                contextOn.Raise();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = false;
            player = null;

            contextOff.Raise();
            dialogBox.SetActive(false);
        }
    }
}
