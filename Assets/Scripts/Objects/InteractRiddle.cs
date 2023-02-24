using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRiddle : Interactable
{
    [Header("Riddle")]
    [SerializeField] private List<RiddleInteractable> interactables = new List<RiddleInteractable>();
    [SerializeField] private bool isSolved;
    [SerializeField] private BoolValue storeRiddleSolved = default;
    private bool isRiddleSolved { get => storeRiddleSolved.RuntimeValue; set => storeRiddleSolved.RuntimeValue = value; }

    [Header("Sound FX")]
    [SerializeField] private AudioClip correctSound = default;
    [SerializeField] private AudioClip incorrectSound = default;

    private List<int> currentOrder = new List<int>();

    protected override void OnEnter(PlayerMovement player)
    {
        if (!isSolved)
        {
            currentOrder.Clear();

            for (int i = 0; i < interactables.Count; i++)
            {
                currentOrder.Add(i + 1);
            }
        }
    }

    public void InteractWithObject(int index)
    {
        if (!isSolved)
        {
            currentOrder.Remove(index);

            if (currentOrder.Count == 0)
            {
                bool isCorrect = true;

                for (int i = 0; i < interactables.Count; i++)
                {
                    if (interactables[i].objectIndex != i + 1)
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if (isCorrect)
                {
                    isSolved = true;
                    isRiddleSolved = true;
                    SoundManager.RequestSound(correctSound);
                    Debug.Log("Riddle solved!");
                    // Add any code to execute when riddle is solved.
                }
                else
                {
                    SoundManager.RequestSound(incorrectSound);
                    Debug.Log("Riddle failed.");
                    // Add any code to execute when riddle is failed.
                }
            }
        }
    }
}