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
        var rigidbody = this.GetComponent<Rigidbody2D>();
        Debug.Log(this.gameObject + " collided with " + other);
        // If you hit an enemy (Which might be moving as well) set anim for hit
        if (other.CompareTag("enemy"))
        {
            animator.SetBool("isFlying", false);
            animator.SetBool("isHit",true);
        }
        // If IceShard is not moving set isHit Issue: Somehow the Velocity is not set to 0
        Debug.Log("Current Velocity is of " + "is" + rigidbody.velocity);
        if (rigidbody.velocity == Vector2.zero)
        {
            animator.SetBool("isHit", true);
        }
    }
    
    
}
