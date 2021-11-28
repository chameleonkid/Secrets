using UnityEngine;
using System;
using System.Collections;

public class ActivateableRune : MonoBehaviour
{
    [SerializeField] private bool activated;
    [SerializeField] private BoolValue runeActive;
    [SerializeField] private AudioClip runeSound;
    [SerializeField] private Material runeMaterial;
    [SerializeField] private Inventory inventory;
    [Header("This item will be needed")]
    [SerializeField] private  Item activateItem;
    [ColorUsageAttribute(true, true)] public Color glowColorActive;
    [ColorUsageAttribute(true, true)] public Color glowColorNotActive;

    public static event Action OnRuneActivated;
    // Start is called before the first frame update
    void Start()
    {
        activated = runeActive.RuntimeValue;
        runeMaterial.SetColor("_GlowColor", glowColorNotActive);
        if (activated)
        {
            ActivateRune();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!activated && other.GetComponent<PlayerMovement>())
        {
            if(activateItem == null || inventory.Contains(activateItem))
            {
                SoundManager.RequestSound(runeSound); // Only play the sound when you walk on it and it wasnt active yet!
                ActivateRune();
                OnRuneActivated?.Invoke(); // Only send the signal in the moment you WALK on it
            }

        }

    }


    private void ActivateRune()
    {
        runeMaterial.SetColor("_GlowColor", glowColorActive);
        activated = true;
        runeActive.RuntimeValue = activated;

    }

   

}
