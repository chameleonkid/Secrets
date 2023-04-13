using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneAfterIntro : MonoBehaviour
{

    public void LoadScene()
    {
        SceneManager.LoadScene("ShipIntro");
    }

}
