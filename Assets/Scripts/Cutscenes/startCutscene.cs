using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startCutscene : MonoBehaviour
{
    [SerializeField] private GameObject currentVCam;
    [SerializeField] private GameObject cutsceneVCam;
    [SerializeField] private float cutsceneDuration;
    [SerializeField] private BoolValue cutSceneBool;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponentInChildren<PlayerMovement>())
        {
            if(cutSceneBool.RuntimeValue == false)
            {
                StartCutscene();
            }
        }
    }


    public void StartCutscene()
    {
        StartCoroutine(StartStopCutscene());
        cutSceneBool.RuntimeValue = true;
    }
    private IEnumerator StartStopCutscene()
    {
        Debug.Log("Cutscene Started");
     //   Time.timeScale = 0;
        currentVCam.SetActive(false);
        cutsceneVCam.SetActive(true);
        yield return new WaitForSecondsRealtime(cutsceneDuration);
        currentVCam.SetActive(true);
        cutsceneVCam.SetActive(false);
        Debug.Log("Cutscene Ended");
    }


}
