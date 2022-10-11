using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip tickingSound;
    [SerializeField] private AudioClip bombDroppedSound;


    void ExplosionSound()
    {
        SoundManager.RequestSound(explosionSound);
    }
    void TickingSoundSound()
    {
        SoundManager.RequestSound(tickingSound);
    }
    void BombDroppedSound()
    {
        SoundManager.RequestSound(bombDroppedSound);
    }

    void DestroyBomb()
    {
        Destroy(this.gameObject);
    }

    new protected void OnHitCollider(Transform other)
    {

    }

    new public void OverrideDamage(float damage)
    {

    }

    new protected void LifetimeCountdown()
    {

    }


}
