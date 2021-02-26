using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public bool active;
    public BoolValue storeValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;
    [Header("Sounds")]
    [SerializeField] private AudioClip switchSound;


    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storeValue.RuntimeValue;
        if (active)
        {
            ActivateSwitch();
        }
    }


    public void ActivateSwitch()
    {
        SoundManager.RequestSound(switchSound);
        active = true;
        storeValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() && !active)
        {
            other.GetComponent<PlayerMovement>().LockMovement(0.25f);
            ActivateSwitch();
        }
    }
}
