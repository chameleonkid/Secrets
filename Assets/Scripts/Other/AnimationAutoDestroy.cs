using UnityEngine;
using System.Collections;

public class AnimationAutoDestroy : MonoBehaviour
{
    public float delay = 0f;
    public GameObject spellParent;

    // Use this for initialization
    void Start()
    {
        Destroy(spellParent, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}