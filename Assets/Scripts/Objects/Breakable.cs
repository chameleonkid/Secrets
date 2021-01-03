using System.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public LootTable thisLoot;

    private Animator anim;

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
        this.gameObject.SetActive(false);
    }
}
