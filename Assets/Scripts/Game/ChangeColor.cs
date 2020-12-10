using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    [SerializeField] private Color regularColor = default;
    [SerializeField] private SpriteRenderer regularSprite;
    [SerializeField] private Color iceColor = default;
    [SerializeField] private Color fireColor = default;
    // Start is called before the first frame update


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

