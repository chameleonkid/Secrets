using UnityEngine;

/// <summary>
/// Sets the Cycle Offset of the animator component.
/// Gives the illusion of wind moving through the scene, by applying X-based animation delay.
/// </summary>
public class AnimatedTreeAnimOffset : MonoBehaviour
{
    private float animOffset;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        float xo = transform.position.x / 8;
        animOffset = 1 - (xo - Mathf.Floor(xo));
        if (animOffset >= 1)
            animOffset -= 1;
        animator.SetFloat("CycleOffset", animOffset);
    }
}