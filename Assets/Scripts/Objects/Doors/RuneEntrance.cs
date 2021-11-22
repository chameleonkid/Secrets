using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneEntrance : MonoBehaviour
{
    [SerializeField] private AudioClip appearSound;
    [SerializeField] private Animator animator;
    [SerializeField] private BoolValue[] runeActive;
    [SerializeField] private bool entranceActive = false;
    [SerializeField] private Collider2D entranceCollider;
    [SerializeField] private SpriteRenderer entranceRenderer;
    // Start is called before the first frame update


    private void Start()
    {
        ActivateableRune.OnRuneActivated += CheckForOpen;
    }

    private void Awake()
    {
        animator = this.GetComponent<Animator>(); ;
        entranceCollider = this.GetComponent<Collider2D>();
        entranceRenderer = this.GetComponent<SpriteRenderer>();
        entranceCollider.enabled = false;
        entranceRenderer.enabled = false;

        if (AreAllRunesActive() == true)
        {
            ActiveEntrance();
        }

    }

    private void CheckForOpen()
    {
        Debug.Log("A rune was triggered");
        if (AreAllRunesActive() == true)
        {
            ActiveEntrance();
        }
    }


    private void ActiveEntrance()
    {
        entranceActive = true;
        animator.SetTrigger("PortalAppearTrigger");
        SoundManager.RequestSound(appearSound);
        entranceCollider.enabled = true;
        entranceRenderer.enabled = true;
    }

    private bool AreAllRunesActive()
    {
        for (int i = 0; i < runeActive.Length; ++i)
        {
            if (runeActive[i].RuntimeValue == false)
            {
                Debug.Log("At least one Rune was not triggered!");
                return false;
            }
        }
        Debug.Log("All Runes are triggered!");
        return true;
    }


}
