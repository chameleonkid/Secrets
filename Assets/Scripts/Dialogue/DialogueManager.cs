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

    [SerializeField] private TextMeshProUGUI nameText = default;
    [SerializeField] private TextMeshProUGUI dialogueText = default;
    [SerializeField] private GameObject dialoguePanel = default;
    [SerializeField] private Animator animator = default;
    [SerializeField] private GameObject nextButton = default;

    private Queue<string> sentences = new Queue<string>();

    private void OnEnable()
    {
        OnDialogueRequested += StartDialogue;
        OnEndDialogue += EndDialogue;
    }

    private void OnDisable()
    {
        OnDialogueRequested -= StartDialogue;
        OnEndDialogue -= EndDialogue;
    }

    private void StartDialogue(Dialogue dialogue)
    {
        if (CanvasManager.Instance.IsFreeOrActive(dialoguePanel))
        {
            dialoguePanel.SetActive(true);
            animator.SetBool("isActive", true);

            if (nextButton)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(nextButton);
            }

            Time.timeScale = 0;

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
        StartCoroutine(EndFinalDialogue());
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    private IEnumerator EndFinalDialogue()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            animator.SetBool("isActive", false);
            yield return new WaitForSecondsRealtime(0.25f);
            Time.timeScale = 1;
            dialoguePanel.SetActive(false);
            CanvasManager.Instance.RegisterClosedCanvas(dialoguePanel);
        }
    }
}
