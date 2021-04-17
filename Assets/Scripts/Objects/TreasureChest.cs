﻿using UnityEngine;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    [SerializeField] private Item contents = default;
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
        player = GameObject.FindObjectOfType<PlayerMovement>();
        dialogue.npcName = "Chest";
        anim = GetComponent<Animator>();
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }


    }

    private void Update()
    {
        if ((Input.GetButtonDown("Interact") || player.GetInteraction()) && playerInRange && Time.timeScale > 0)
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
        dialogue.sentences[0] = contents.description;
        TriggerDialogue();
        player.inventory.currentItem = contents;
        player.inventory.items[contents]++;

        // raise the signal to animate
        player?.RaiseItem();
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
        dialogue.sentences[0] = "There is no space left in your inventory";
        TriggerDialogue();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = true;
           // player = other.GetComponent<PlayerMovement>();

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
           // player = null;

            contextOff.Raise();
        }
    }

    public void TriggerDialogue() => DialogueManager.RequestDialogue(dialogue);
}
