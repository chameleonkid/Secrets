using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public Animator animator;
    public GameObject NextButton;
    public GameObject inventoryActive;
    public GameObject pauseActive;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        if (!inventoryActive.activeInHierarchy && !pauseActive.activeInHierarchy)
        {


            if (NextButton)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(NextButton);
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
        else
        {
            return;
        }
    }

    //Submits the sentences via FIFO if there are sentences available
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
        Time.timeScale = 1;
    }


    IEnumerator TypeSentence (string sentence)
    {
        animator.SetBool("isActive", true);
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
           // yield return new WaitForSeconds(0.05f);   Looks way better, but doesnt work with timescale = 0;
            yield return null;
        }
    }


}
