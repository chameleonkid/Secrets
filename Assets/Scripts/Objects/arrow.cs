using UnityEngine;

public class arrow : ProjectileTemp
{
    public BoxCollider2D arrowcollider;

    protected override float destroyTime => 1;
    protected override Collider2D projectileCollider => arrowcollider;
}
