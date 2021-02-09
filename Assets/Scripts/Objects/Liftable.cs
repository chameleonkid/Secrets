﻿using UnityEngine;

public class Liftable : Interactable
{
    public Item contents;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private int neededStrenght;
    [SerializeField] private AudioClip liftingSound;
    [SerializeField] private Dialogue dialogue = default;

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Lift") && playerInRange && Time.timeScale > 0)
        {
            if(playerInventory.items.HasCapacity(contents))
            {
                Lifting();
            }
            else
            {
                NoInventorySpace();
            }

        }
    }

    //############### TEST-Lift ###################################
    public void Lifting()
    {

        if (playerInventory.currentGloves && playerInventory.currentGloves.strength >= neededStrenght)
        {
            playerInventory.items[contents]++;
            Destroy(this.gameObject);
            if(liftingSound)
            {
                SoundManager.RequestSound(liftingSound);
            }

        }
        else
        {
            dialogue.sentences[0] = "You don't seem strong enaugh to lift this up...";
            DialogueManager.RequestDialogue(dialogue);
        }
      
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOn.Raise();
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOff.Raise();
            playerInRange = false;

        }
    }


    public void NoInventorySpace()
    {
        dialogue.sentences[0] = "There is no space left in your inventory";
        TriggerDialogue();
    }

    public void TriggerDialogue() => DialogueManager.RequestDialogue(dialogue);

}
