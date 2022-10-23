using UnityEngine;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    [SerializeField] private Item contents = default;
    [SerializeField] private int amount= 1;
    [SerializeField] private BoolValue storeOpen = default;
    private bool isOpen { get => storeOpen.RuntimeValue; set => storeOpen.RuntimeValue = value; }

    [Header("Dialogue")]
    [SerializeField] private Dialogue dialogue = default;

    [Header("Sound FX")]
    [SerializeField] private AudioClip chestSound = default;
    [SerializeField] private AudioClip noInventorySpace = default;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (isOpen)
        {
            if(anim)
            {
                anim.SetBool("opened", true);
            }
        }
    }

    private void Update()
    {
        if (playerInRange && player.inputInteract && Time.timeScale > 0)
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
                player?.RaiseItem();
            }
        }
    }

    public void OpenChest()
    {
        dialogue.lines[0].text = contents.description;
        TriggerDialogue();
        player.inventory.currentItem = contents;
        for(int i=1;i<=amount;i++)
        {
            player.inventory.items[contents]++;
        }
        // raise the signal to animate
        player?.RaiseItem();
        // set the chest to opened
        isOpen = true;
        SoundManager.RequestSound(chestSound);
        //raise the context clue to off
        contextOff.Raise();
        if(anim)
        {
            anim.SetBool("opened", true);
        }

    }

    public void NoInventorySpace()
    {
        SoundManager.RequestSound(noInventorySpace);
        dialogue.lines[0].text = "There is no space left in your inventory";
        TriggerDialogue();
    }

    protected override void OnEnter(PlayerMovement player)
    {
        this.player = player;
        if (!isOpen)
        {
            contextOn.Raise();
        }
    }

    public void TriggerDialogue() => DialogueManager.RequestDialogue(dialogue);
}
