using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWalls : MonoBehaviour
{
    public BoolValue storeOpen;
    public bool open = false;

    private void Start()
    {
        open = storeOpen.RuntimeValue;
        if (open == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    [SerializeField] private AudioClip openSound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bomb>())
        {
            SoundManager.RequestSound(openSound);
            this.gameObject.SetActive(false);
            open = true;
            storeOpen.RuntimeValue = true;
        }
    }
}
