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


    // Start is called before the first frame update
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
        active = true;
        storeValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Is it the player?
        if (other.GetComponent<PlayerMovement>())
        {
            ActivateSwitch();
        }
    }
}
