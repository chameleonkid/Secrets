using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerOnTrigger : MonoBehaviour
{
    public float freezeTimer;
    [SerializeField] private EnemyFrozenSetter enemyFrozenSetter;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInChildren<EnemyFrozenSetter>() && other.GetComponent<Enemy>())
        {
            enemyFrozenSetter = other.GetComponentInChildren<EnemyFrozenSetter>();
            enemyFrozenSetter.SetFreezeDuration(freezeTimer);
            enemyFrozenSetter.SetFreeze();
        }
    }
}
