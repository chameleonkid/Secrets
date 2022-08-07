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

    private Dialogue dialogue;
    private int lineIndex;

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

    private void SelectNextButton()
    {
        if (nextButton)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(nextButton);
        }
    }

    private void StartDialogue(Dialogue dialogue)
    {
        if (CanvasManager.Instance.IsFreeOrActive(dialoguePanel))
        {
            dialoguePanel.SetActive(true);
            animator.SetBool("isActive", true);
            SelectNextButton();
            Time.timeScale = 0;

            this.dialogue = dialogue;
            lineIndex = 0;

            DisplayNextLine();
        }
    }

    //Submits the sentences via FIFO if there are sentences available
    // Called from `ContinueButton` in `DialogueCanvas`
    public void DisplayNextLine()
    {
        if (lineIndex < dialogue.lines.Length)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogue.lines[lineIndex].text));
            nameText.text = dialogue.speakers[dialogue.lines[lineIndex].speakerIndex].name;
        }
        else
        {
            EndDialogue();
        }
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
