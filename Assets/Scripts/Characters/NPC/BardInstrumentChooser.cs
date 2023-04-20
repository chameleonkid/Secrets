using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardInstrumentChooser : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public enum BardInstrument
    {
        Guitar,
        Violin,
        Singing,
        Cheering,  
    }

    [SerializeField] private BardInstrument instrumentChooser = BardInstrument.Singing;


    // Start is called before the first frame update
    private void Awake()
    {
        animator = this.GetComponent<Animator>();

        switch (instrumentChooser)
        {
            case BardInstrument.Guitar:
                animator.SetBool("playingGuitar", true);
                animator.SetBool("isSinging", false);
                animator.SetBool("playingViolin", false);
                animator.SetBool("isCheering", false);
                break;
            case BardInstrument.Violin:
                animator.SetBool("playingGuitar", false);
                animator.SetBool("isSinging", false);
                animator.SetBool("playingViolin", true);
                animator.SetBool("isCheering", false);
                break;
            case BardInstrument.Singing:
                animator.SetBool("playingGuitar", false);
                animator.SetBool("isSinging", true);
                animator.SetBool("playingViolin", false);
                animator.SetBool("isCheering", false);
                break;
            case BardInstrument.Cheering:
                animator.SetBool("playingGuitar", false);
                animator.SetBool("isSinging", false);
                animator.SetBool("playingViolin", false);
                animator.SetBool("isCheering", true);
                break;
            default:
                break;
        }
    }
}

