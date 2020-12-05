using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SzeneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerPosMemory;
//    public Vector2 cameraNewMax;
//    public Vector2 cameraNewMin;
//    public VectorValue cameraMin;
//    public VectorValue cameraMax;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;




    public void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
      //    ResetCameraBounds(); //evtl nicht richtig!!!!

        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerPosMemory.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

/*
    public void ResetCameraBounds()
    {
        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
    }
*/
}
