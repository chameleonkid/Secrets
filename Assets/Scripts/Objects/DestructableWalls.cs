using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWalls : MonoBehaviour
{

    [SerializeField] private AudioClip openSound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bomb>())
        {
            SoundManager.RequestSound(openSound);
            this.gameObject.SetActive(false);
        }
    }
}
