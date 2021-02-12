using System.Collections;
using UnityEngine;

public class ShrinkOnTrigger : Hitbox
{
    [SerializeField] private float shrinkPercentValue = 0.5f;
    [SerializeField] private float shrinkDuration = 2;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<Character>();
        Debug.Log(other + " was hit by shrinkspell");
        if (hit != null && hit.gameObject.activeInHierarchy)
        {
            hit.Shrink(shrinkPercentValue, shrinkDuration);
        }
    }

}
