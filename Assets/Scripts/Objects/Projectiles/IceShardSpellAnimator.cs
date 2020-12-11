using UnityEngine;

public class IceShardSpellAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator= transform.GetComponent<Animator>();
        animator.SetBool("isFlying", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            animator.SetBool("isFlying", false);
        }
    }
}
