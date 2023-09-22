using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFrozenSetter : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private SpriteRenderer sprite;
    [Header("This is set by the FreezeOnTrigger.cs")]
    [SerializeField] private float freezeDuration;
    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        enemy = this.GetComponentInParent<Enemy>();

    }

    public void SetFreezeDuration(float duration)
    {
        freezeDuration = duration;
    }

    public void SetFreeze()
    {
        StartCoroutine(FreezeAnimCo(freezeDuration));
    }

    protected virtual IEnumerator FreezeAnimCo(float freezeDuration)
    {
        sprite.enabled = true;
        enemy.SetFreeze(freezeDuration);
        yield return new WaitForSeconds(freezeDuration);
        sprite.enabled = false;
    }

}
