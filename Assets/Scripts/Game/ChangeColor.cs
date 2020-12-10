using System.Collections;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    [SerializeField] private Color regularColor;
    [SerializeField] private SpriteRenderer regularSprite = default;
    [SerializeField] private Color iceColor = Color.blue;
    [SerializeField] private Color fireColor = Color.red;

    private void Start()
    {
        regularSprite = this.GetComponent<SpriteRenderer>();
        regularColor = new Color(255f, 255f, 255f, 1f);
    }

    public void changeColorToBlue(float timer)
    {
        //regularEnemyColor = enemySprite.color; this was dumb... doublehits change the color forever :)
        StartCoroutine(IceCo(timer));
    }

    public void changeColorToRed(float timer)
    {
        //regularEnemyColor = enemySprite.color; this was dumb... doublehits change the color forever :)
        StartCoroutine(FireCo(timer));
    }


    private IEnumerator IceCo(float timer)
    {
        regularSprite.color = iceColor;
        yield return new WaitForSeconds(timer);
        regularSprite.color = regularColor;
    }

    private IEnumerator FireCo(float timer)
    {
        regularSprite.color = fireColor;
        yield return new WaitForSeconds(timer);
        regularSprite.color = regularColor;
    }
}
