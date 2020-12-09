using UnityEngine;

public class SpellFireball : ProjectileTemp
{
    public BoxCollider2D spellCollider;

    protected override float destroyTime => 5;  // I need to get the DotTime somehow... AFTER IT IS SET!
    protected override Collider2D projectileCollider => spellCollider;
}
