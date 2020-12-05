using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SimpleSave : MonoBehaviour
{
    public GameObject managerScript;
    public GameObject pausePanel;
    

    private string levelToLoad;
    AsyncOperation asyncLoadLevel;

    public void Save()
    {
        GameSaveManager script = managerScript.GetComponent<GameSaveManager>();

        script.playerPosition.initialValue.x = script.currentPlayerPosition.transform.position.x;
        script.playerPosition.initialValue.y = script.currentPlayerPosition.transform.position.y;
        ES3.Save("Position", script.playerPosition);


        //################# Save the Current Scene #########################
        ES3.Save("Scene", SceneManager.GetActiveScene().name);
        ES3.Save("Position",script.playerPosition);


        ES3.Save("Chests", script.chests);
        ES3.Save("Inventory", script.playerInventory);
        ES3.Save("Health", script.playerHealth);
        ES3.Save("Heartcontainers", script.HeartContainers);

       

        //############ Save ALL SCRIPTABLE OBJECTS NUMBER HELD Cause otherwise they lose the OnClickEvent ######################
        ES3.Save("BluePotions", script.bluePotion.numberHeld);
        ES3.Save("RedPotions", script.redPotion.numberHeld);
        ES3.Save("GreenPotions", script.greenPotion.numberHeld);
        ES3.Save("YellowPotions", script.yellowPotion.numberHeld);
        ES3.Save("Arrows", script.arrows.numberHeld);
        ES3.Save("SmallKeys", script.smallKeys.numberHeld);



    }

    public void Load()
    {
        //################# Load the scene ####################
        levelToLoad = ES3.Load<string>("Scene");
        SceneManager.LoadScene(levelToLoad);

        //################# Load all safed objects #################
        GameSaveManager script = managerScript.GetComponent<GameSaveManager>();
        script.playerPosition = ES3.Load("Position", script.playerPosition);
        script.chests = ES3.Load("Chests", script.chests);
        script.playerInventory = ES3.Load("Inventory", script.playerInventory);
        script.playerHealth = ES3.Load("Health", script.playerHealth);
        script.HeartContainers = ES3.Load("Heartcontainers", script.HeartContainers);


        //################## Load all Scriptable Objects / Numberheld #################################
        script.bluePotion.numberHeld = ES3.Load("BluePotions", script.bluePotion.numberHeld);
        script.redPotion.numberHeld = ES3.Load("RedPotions", script.redPotion.numberHeld);
        script.greenPotion.numberHeld = ES3.Load("GreenPotions", script.greenPotion.numberHeld);
        script.yellowPotion.numberHeld = ES3.Load("YellowPotions", script.yellowPotion.numberHeld);
        script.arrows.numberHeld = ES3.Load("Arrows", script.arrows.numberHeld);
        script.smallKeys.numberHeld = ES3.Load("SmallKeys", script.smallKeys.numberHeld);


        //Refresh Screen
        script.resetHealth();
        script.heartSignal.Raise();
        script.arrowSignal.Raise();
        script.coinSignal.Raise();

        // PauseMenü schließen und Zeit weiterlaufen lassen
        pausePanel.SetActive(false);
        Time.timeScale = 1f;



    }
}
