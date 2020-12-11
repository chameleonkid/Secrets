using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeColor : MonoBehaviour
{
    private new SpriteRenderer renderer;
    private Color regularColor;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        regularColor = renderer.color;
    }

    public void ChangeSpriteColor(Color color, float duration) {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(ChangeSpriteColorCo(color, duration));
        }
    }

    private IEnumerator ChangeSpriteColorCo(Color color, float duration)
    {
        renderer.color = color;
        yield return new WaitForSeconds(duration);
        renderer.color = regularColor;
    }
}
