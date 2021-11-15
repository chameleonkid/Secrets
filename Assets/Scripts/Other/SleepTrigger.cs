using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepTrigger : MonoBehaviour
{

    [SerializeField] private FloatValue normalizedTimeOfDay = default;
    [SerializeField] private ConstrainedFloat playerHealth = default;
    [SerializeField] private ConstrainedFloat playerMana = default;
    [SerializeField] private Animator anim = default;
    [SerializeField] private PlayerMovement player = default;
    [SerializeField] private Vector3 newPlayerPos = default;

    public void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        
        if (other.GetComponentInChildren<PlayerMovement>())
        {
            normalizedTimeOfDay.value = 0.25f;
            playerHealth.current = playerHealth.max;
            playerMana.current = playerMana.max;
            StartCoroutine(StartSleeping());
      
        }



    }

    private IEnumerator StartSleeping()
    {
        anim.SetTrigger("StartSleeping");
        yield return new WaitForSeconds(1f);
        newPlayerPos = player.transform.position;
        newPlayerPos.x -= 0.5f;
        player.transform.position = newPlayerPos;
        anim.SetTrigger("StopSleeping");

    }

}


