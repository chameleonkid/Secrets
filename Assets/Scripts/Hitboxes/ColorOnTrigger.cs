using UnityEngine;

public class ColorOnTrigger : Hitbox
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private float duration = 2;

    protected override void OnHit(Collider2D other)
    {
        // Should this be a coroutine like `SlowOnTrigger` or `DamageOverTime`
        // using `GetComponent<SpriteRenderer>()` instead?
        // Perhaps this the current implementation is favourable,
        // as it grants control over which sprites are able to have their color changed.
        var hit = other.GetComponent<ChangeColor>();
        if (hit != null)
        {
            hit.ChangeSpriteColor(color, duration);
        }
    }
}
