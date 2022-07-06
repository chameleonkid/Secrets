using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AreaNameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI areaNameDisplay;
    [SerializeField] private string areaNameText;

    // Start is called before the first frame update

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (areaNameDisplay && areaNameDisplay.text != "" && areaNameDisplay)
            {
                StartCoroutine(ShowAreaNameCo());
            }
        }
    }

    IEnumerator ShowAreaNameCo()
    {
        areaNameDisplay.text = areaNameText;
        areaNameDisplay.enabled = true;
        yield return new WaitForSeconds(5f);
        areaNameDisplay.enabled = false;
    }
}
