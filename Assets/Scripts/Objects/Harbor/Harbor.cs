using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Harbor : MonoBehaviour
{
    [SerializeField] private AudioClip harborSound;
    [SerializeField] private ShipMovement myShip;
    [SerializeField] private VectorValue playerPos;
    [Header("This is the position the player will be")]
    [SerializeField] private Vector2 HarborPlayerPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.myShip = other.GetComponent<ShipMovement>();
        Debug.Log("isInHarborRange");
        if (other.GetComponent<ShipMovement>() && other.isTrigger)
        {
            if (harborSound)
            {
                SoundManager.RequestSound(harborSound); //When you enter the area to land, the bell rings
                Debug.Log("ENTER HARBOR");

            }
            // I want this to happen on Shipmovement Interact
            playerPos.value = HarborPlayerPos;
            SceneManager.LoadScene("Overworld");
        }

    }
}
