using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {

        nameText.text = dialogue.npcName;
        sentences.Clear();
        dialoguePanel.SetActive(true);
        animator.SetBool("isActive", true);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

   public void EndDialogue()
    {
        animator.SetBool("isActive", false);    
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
