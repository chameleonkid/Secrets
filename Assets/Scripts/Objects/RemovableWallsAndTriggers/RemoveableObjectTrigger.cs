using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveableObjectTrigger : MonoBehaviour
{
    [Header("This object will be removed if the trigger is reached")]
    [SerializeField] private GameObject objectToRemove;
    [Header("SO to save the state")]
    [SerializeField] private BoolValue objectRemovedSave;
    [Header("Swap the look of the trigger")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite triggerActiveSprite;
    [Header("Make a sound and trigger a Dialoge")]
    [SerializeField] private AudioClip triggerSound;
    [SerializeField] private Dialogue triggerDialogue;



    public void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        if (objectRemovedSave.RuntimeValue == true)
        {
            objectToRemove.SetActive(false);
            spriteRenderer.sprite = triggerActiveSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>() && other.isTrigger && objectRemovedSave.RuntimeValue == false)
        {
            TriggerRemove();
        }
    }

    public void TriggerRemove()
    {
        spriteRenderer.sprite = triggerActiveSprite;
        SoundManager.RequestSound(triggerSound);
        objectToRemove.SetActive(false);
        DialogueManager.RequestDialogue(triggerDialogue);
        objectRemovedSave.RuntimeValue = true;
    }

}
