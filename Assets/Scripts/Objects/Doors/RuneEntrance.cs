using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneEntrance : MonoBehaviour
{
    [SerializeField] private AudioClip appearSound;
    [SerializeField] private Animator animator;
    [SerializeField] private BoolValue[] runeActive;
    [SerializeField] private bool entranceActive = false;
    [SerializeField] private BoxCollider2D transitionCollider;
    [SerializeField] private Collider2D entranceCollider;
    [SerializeField] private SpriteRenderer entranceRenderer;

    [Header("This will set the current VCam Inactive and the CutsceneCam Active")]
    [SerializeField] private GameObject currentVCam;
    [SerializeField] private GameObject cutsceneVCam;
    [SerializeField] private float cutsceneDuration;
    // Start is called before the first frame update


    private void Start()
    {
        ActivateableRune.OnRuneActivated += CheckForOpen;
    }

    private void Awake()
    {
        animator = this.GetComponent<Animator>(); ;

        entranceCollider = this.GetComponent<PolygonCollider2D>();
        entranceRenderer = this.GetComponent<SpriteRenderer>();
        transitionCollider.enabled = false;
        entranceCollider.enabled = false;
        entranceRenderer.enabled = false;

        if (AreAllRunesActive() == true)
        {
            ActiveEntrance();
        }

    }

    private void CheckForOpen()
    {
        Debug.Log("A rune was triggered");
        if (AreAllRunesActive() == true)
        {
            ActiveEntrance();
        }
    }


    private void ActiveEntrance()
    {
        StartCoroutine(ShowAndOpen());

    }

    private bool AreAllRunesActive()
    {
        for (int i = 0; i < runeActive.Length; ++i)
        {
            if (runeActive[i].RuntimeValue == false)
            {
                Debug.Log("At least one Rune was not triggered!");
                return false;
            }
        }
        Debug.Log("All Runes are triggered!");
        return true;
    }


    private IEnumerator ShowAndOpen()
    {
        Debug.Log("Cutscene Started");
        currentVCam.SetActive(false);
        cutsceneVCam.SetActive(true);
        SoundManager.RequestSound(appearSound); // This is triggered every Time the scene is loaded and ALL Runes are active already... makes sense... needs to be fixed
        entranceActive = true;
        animator.SetTrigger("PortalAppearTrigger");
        transitionCollider.enabled = true;
        entranceCollider.enabled = true;
        entranceRenderer.enabled = true;
        yield return new WaitForSecondsRealtime(cutsceneDuration);
        currentVCam.SetActive(true);
        cutsceneVCam.SetActive(false);
        Debug.Log("Cutscene Ended");
    }


}
