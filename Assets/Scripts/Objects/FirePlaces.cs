using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePlaces : Interactable
{
    public GameObject firePlace;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

        //####################### Set firePlace on #################################################
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            firePlace.SetActive(!firePlace.activeInHierarchy);   
        }

    }


}
