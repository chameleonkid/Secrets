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

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<FreezerOnTrigger>())
        {
            freezeDuration = other.GetComponent<FreezerOnTrigger>().FreezeTimer;
            SetFreeze();
        }
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
