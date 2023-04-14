using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class startCutscene : MonoBehaviour
{
    [SerializeField] private GameObject currentVCam;
    [SerializeField] private GameObject cutsceneVCam;
    [SerializeField] private float cutsceneDuration;
    [SerializeField] private BoolValue cutSceneBool;
    [Header("Use this only when the player must not be moved or animated")]
    [SerializeField] private BoolValue cantMove;
    public PlayableDirector playableDirector;
    public TimelineAsset timeLineToPlay;


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
        if (timeLineToPlay)
        {
            playableDirector.Play(timeLineToPlay);
        }
        cutSceneBool.RuntimeValue = true;

    }
    private IEnumerator StartStopCutscene()
    {
        Debug.Log("Cutscene Started");
        //   Time.timeScale = 0;
        if(cantMove)
        {
            cantMove.RuntimeValue = true;
        }

        currentVCam.SetActive(false);
        cutsceneVCam.SetActive(true);
        yield return new WaitForSecondsRealtime(cutsceneDuration);

        currentVCam.SetActive(true);
        cutsceneVCam.SetActive(false);
        if(cantMove)
        {
            cantMove.RuntimeValue = false;
        }
        Debug.Log("Cutscene Ended");
    }


}
