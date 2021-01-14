using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;


    public void SetLetter(string letter)
    {
        var name = playerName.text;
        if(name == "Your Name")
        {
            playerName.text = letter ;
        }
        else if(name.Length > 10)
        {
            
        }
        else
        {
            playerName.text += letter;
        }  
    }

    public void RemoveLetter()
    {
        var name = playerName.text;
        if (name == "Your Name")
        {
            playerName.text = "";
        }
        else if (name.Length <  1)
        {
            playerName.text = "";
        }
        else
        {
            playerName.text = playerName.text.Remove(playerName.text.Length - 1);
        }

    }

}
