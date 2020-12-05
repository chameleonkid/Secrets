using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public Enemy[] enemies;
    public Breakable[] breakables;
    public GameObject virtualCamera;



    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
     //       Debug.Log(this.gameObject.name);
       //     Debug.Log(other.gameObject.name);
            virtualCamera.SetActive(true);

            //Activate all enemies
            for(int i = 0; i < enemies.Length; i++)
            {
                changeActivation(enemies[i], true);
                enemies[i].chaseRadius = enemies[i].originalChaseRadius; //Set Chaseradius on Respawn
            }
            //Activate all breakables
            for (int i = 0; i < breakables.Length; i++)
            {
                changeActivation(breakables[i], true);
            }

        }
    }


    public virtual void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(false);
            //DEactivate all enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                changeActivation(enemies[i], false);
            }
            //DEactivate all breakables
            for (int i = 0; i < breakables.Length; i++)
            {
                changeActivation(breakables[i], false);
            }

        }

    }

   public void changeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }



    public void OnDisable()
    {
        virtualCamera.SetActive(false);
    }

}
