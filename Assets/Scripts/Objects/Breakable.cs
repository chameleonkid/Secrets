using System.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public LootTable thisLoot;
    private Animator anim;
    [SerializeField] private AudioClip switchSound;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Smash()
    {
        anim.SetBool("Smash", true);
        StartCoroutine(breakCo());
        thisLoot?.GenerateLoot(transform.position);
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.RequestSound(switchSound);
        this.gameObject.SetActive(false);
    }
}
