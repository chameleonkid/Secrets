using UnityEngine;

public class DungeonEnemyRoom : Room
{
    public Door[] doors;

    //  ######################################### Function to check for enemies alive(active) #########################################
    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1 )
            {
                return;
            }
        }
        OpenDoors();
    }


    //  ######################################### Function reset the rooms on Entering it #########################################

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(true);
            //Activate all enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                changeActivation(enemies[i], true);
                enemies[i].chaseRadius = enemies[i].originalChaseRadius;
            }
            //Activate all breakables
            for (int i = 0; i < breakables.Length; i++)
            {
                changeActivation(breakables[i], true);
            }
            CloseDoors();
        }
    }

    //  ######################################### Function deacticate the rooms on leaving it #########################################

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
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
            virtualCamera.SetActive(false);
        }
    }

    //  ######################################### Function to open all doors #########################################

    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }
    //  ######################################### Function to open all doors #########################################

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }
}
