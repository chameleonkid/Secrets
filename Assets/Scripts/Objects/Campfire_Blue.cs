using UnityEngine;
using System;
using System.Collections;

public class Campfire_Blue : Interactable
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private AudioClip lightingSound;
    [SerializeField] private Animator animator;
    [SerializeField] private Item itemToLightWith;
    [SerializeField] private BoolValue fireLightedSave;
    [SerializeField] private Dialogue dialogueSuccessfull = default;
    [SerializeField] private Dialogue dialogueMissingItem = default;
    [SerializeField] private Dialogue dialogueAlreadyLit = default;

    public static event Action OnFireLit;


    public void Awake()
    {
        animator = this.GetComponent<Animator>();
        if(fireLightedSave.RuntimeValue == true)
        {
            animator.SetTrigger("isBurning");
        }
    }

    private void LateUpdate()
    {
        if (playerInRange && player.inputInteract && Time.timeScale > 0)
        {
            if (playerInventory.Contains(itemToLightWith) && fireLightedSave.RuntimeValue == false)
            {
                playerInventory.items[itemToLightWith]--;
                animator.SetTrigger("isBurning");
                TriggerDialogue(dialogueSuccessfull);
                fireLightedSave.RuntimeValue = true;
                Debug.Log("This fire was lit");
                OnFireLit?.Invoke();
                Debug.Log("Fire Lit Event is triggered");
            }
            if(fireLightedSave.RuntimeValue == true)
            {
                TriggerDialogue(dialogueAlreadyLit);
            }
            else
            {
                TriggerDialogue(dialogueMissingItem);
            }
        }
    }


    public void TriggerDialogue(Dialogue dialogue) => DialogueManager.RequestDialogue(dialogue);
}
