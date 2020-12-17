using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class DialogueManager : MonoBehaviour
{
    private static event Action<Dialogue> OnDialogueRequested;
    public static void RequestDialogue(Dialogue dialogue) => OnDialogueRequested?.Invoke(dialogue);

    private static event Action OnEndDialogue;
    public static void RequestEndDialogue() => OnEndDialogue?.Invoke();
    
    private Queue<string> sentences = new Queue<string>();

    [SerializeField] private TextMeshProUGUI nameText = default;
    [SerializeField] private TextMeshProUGUI dialogueText = default;
    [SerializeField] private GameObject dialoguePanel = default;
    [SerializeField] private Animator animator = default;
    [SerializeField] private GameObject nextButton = default;
    [SerializeField] private GameObject inventoryActive = default;
    [SerializeField] private GameObject pauseActive = default;

    private void OnEnable() {
        OnDialogueRequested += StartDialogue;
        OnEndDialogue += EndDialogue;
    }

    private void OnDisable() {
        OnDialogueRequested -= StartDialogue;
        OnEndDialogue -= EndDialogue;
    }

    private void StartDialogue(Dialogue dialogue)
    {
        if (!inventoryActive.activeInHierarchy && !pauseActive.activeInHierarchy)
        {
            if (nextButton)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(nextButton);
            }

            Time.timeScale = 0;
            nameText.text = dialogue.npcName;
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }
    }

    //Submits the sentences via FIFO if there are sentences available
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private void EndDialogue()
    {
        animator.SetBool("isActive", false);
        Time.timeScale = 1;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        animator.SetBool("isActive", true);
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
