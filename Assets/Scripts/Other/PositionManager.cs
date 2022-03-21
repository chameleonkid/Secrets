using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PositionManager : MonoBehaviour
{

    public Transform playerTransform;
    public Text posText;
    // Start is called before the first frame update
    void Start()
    {
       if(playerTransform == null)
        {
            playerTransform = GameObject.FindObjectOfType<PlayerMovement>().transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        string x = playerTransform.transform.position.x.ToString("000.0");
        string y = playerTransform.transform.position.y.ToString("000.0");
        posText.text ="Scene: " + SceneManager.GetActiveScene().name + "\nCoordinates: \nx: " + x + "\ny: " + y;
    }
}
