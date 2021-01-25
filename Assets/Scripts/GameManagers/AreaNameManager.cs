using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AreaNameManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro areaNameDisplay;
    [SerializeField] private string areaNameText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowAreaNameCo());
    }

    IEnumerator ShowAreaNameCo()
    {
        this.gameObject.SetActive(true);
        areaNameDisplay.text = areaNameText;
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);

    }
}
