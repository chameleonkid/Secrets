using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
        UpdateHearts();
    }

    public void InitHearts()
    {
        if (heartContainers.RuntimeValue > heartContainers.initialValue) 
        {
            for (int i = 0; i < heartContainers.RuntimeValue; i++)
            {
          //      if (i < hearts.Length)
          //      {
                    hearts[i].gameObject.SetActive(true);
                    hearts[i].sprite = fullHeart;
          //      }
            }
        }
        else                                                       
        {
           foreach (Image heart in hearts)
            {
                heart.gameObject.SetActive(false);
            }
            for ( int i = 0; i < heartContainers.initialValue; i++)
            {
             //   if (i < hearts.Length)
             //   {
                    hearts[i].gameObject.SetActive(true);
                    hearts[i].sprite = fullHeart;
             //   }
            }
        }
    }

    public void UpdateHearts()
    {
        InitHearts();
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        for (int i = 0; i < heartContainers.RuntimeValue; i ++)
        {
            if(i <= tempHealth-1)
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
            }
            else if( i >= tempHealth)
            {
                // Empty Heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //Halffull Heart;
                hearts[i].sprite = halfFullHeart;
            }
        {
              
            }
        }
    }


    //############### Herzen dazu Test #####################
    /*
    public void addHearts()
    {
        heartContainers.initialValue = heartContainers.initialValue + 1;
    }
    */
    //############### Herzen dazu Test #####################

}
