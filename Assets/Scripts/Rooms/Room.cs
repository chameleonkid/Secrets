using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Room : MonoBehaviour
{
    [SerializeField] protected GameObject virtualCamera = default;
    [SerializeField] protected Enemy[] enemies = default;
    [SerializeField] protected GameObject npcs = default;
    [SerializeField] protected Breakable[] breakables = default;
    [SerializeField] protected ParticleSystem[] particleSystems = default;
    [Header("AreaNameDisplay")]
    [SerializeField] protected string  areaName = default;
    [SerializeField] protected TextMeshProUGUI areaNameText = default;




    public void OnDisable() => virtualCamera.SetActive(false);

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SetActiveRoom(true);
            if(areaNameText && areaNameText.text != "" && areaNameText)
            {
                StartCoroutine(ShowAreaTextCo());
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SetActiveRoom(false);
        }
    }

    protected void SetActiveRoom(bool value)
    {
        virtualCamera.SetActive(value);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(value);
        }

        for (int i = 0; i < breakables.Length; i++)
        {
            breakables[i].gameObject.SetActive(value);
        }

        if(particleSystems.Length > 0)
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].gameObject.SetActive(value);
            }
        }

        if(npcs)
        {
            npcs.SetActive(value);
        }
  
    }

    IEnumerator ShowAreaTextCo()
    {
        areaNameText.text = areaName;
        areaNameText.enabled = true;
        yield return new WaitForSeconds(5f);
        areaNameText.enabled = false;
    }

}
