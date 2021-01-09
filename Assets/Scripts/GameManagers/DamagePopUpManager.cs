using System;
using TMPro;
using UnityEngine;

public class DamagePopUpManager : MonoBehaviour
{
    private static event Action<float, bool, Transform> OnDamagePopUpRequested;

    public static void RequestDamagePopUp(float damage, Transform hit) => OnDamagePopUpRequested?.Invoke(damage, false, hit);
    public static void RequestDamagePopUp(float damage, bool isCritical, Transform hit) => OnDamagePopUpRequested?.Invoke(damage, isCritical, hit);

    private static event Action<float, Transform> OnHealPopUpRequested;
    public static void RequestHealPopUp(float heal, Transform toBeHealed) => OnHealPopUpRequested?.Invoke(heal, toBeHealed);

    [SerializeField] private GameObject regularPopUpPrefab = default;
    [SerializeField] private GameObject criticalPopUpPrefab = default;
    [SerializeField] private GameObject healPopUpPrefab = default;
    [SerializeField] private float displayDuration = 0.5f;

    private void OnEnable()
    {
        OnDamagePopUpRequested += InstantiatePopUp;
        OnHealPopUpRequested += InstantiateHealPopUp;
    }
    private void OnDisable()
    {
        OnDamagePopUpRequested -= InstantiatePopUp;
        OnHealPopUpRequested -= InstantiateHealPopUp;
    }


    private void InstantiatePopUp(float damage, bool isCritical, Transform hit)
    {
        var prefab = isCritical ? criticalPopUpPrefab : regularPopUpPrefab;
        var popup = Instantiate(prefab, hit.position, Quaternion.identity, hit);
        var text = popup.GetComponent<TextMeshPro>();
        if (text != null)
        {
            text.text = damage.ToString();
        }
        Destroy(popup.gameObject, displayDuration);
    }

    private void InstantiateHealPopUp(float heal, Transform toBeHealed)
    {
        var prefab = healPopUpPrefab;
        var popup = Instantiate(prefab, toBeHealed.position, Quaternion.identity, toBeHealed);
        var text = popup.GetComponent<TextMeshPro>();
        if (text != null)
        {
            text.text = heal.ToString();
        }
        Destroy(popup.gameObject, displayDuration);
    }

}
