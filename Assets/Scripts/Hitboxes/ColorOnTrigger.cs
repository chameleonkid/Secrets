using UnityEngine;

public class ColorOnTrigger : Hitbox
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private float duration = 2;

    protected override void OnHit(Collider2D other)
    {
        var hit = other.GetComponent<ChangeColor>();
        if (hit != null)
        {
            hit.ChangeSpriteColor(color, duration);
        }
    }
}
