using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public InventoryItem contents;
    public PlayerInventory playerInventory;
    public bool isOpen;
    public BoolValue storeOpen;

    [Header("Signals + Dialog")]
    public Signals raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    private Animator anim;

    public SoundManager soundManager;
    
    


    // Start is called before the first frame update
    void Start()
    {
        isOpen = storeOpen.RuntimeValue;
        anim = GetComponent<Animator>();
        if(isOpen)
        {
            anim.SetBool("opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if(!isOpen)
            {          
                OpenChest();      
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
        dialogText.text = contents.itemDescription;

        playerInventory.currentItem = contents;
        playerInventory.Add(contents);

        // raise the signal to animate
        
        raiseItem.Raise();
        // set the chest to opened
        isOpen = true;
        //soundManager.PlayChestSound();
        //raise the context clue to off
        contextOff.Raise();
        anim.SetBool("opened", true);
        storeOpen.RuntimeValue = isOpen; 
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
        }

    }

}

