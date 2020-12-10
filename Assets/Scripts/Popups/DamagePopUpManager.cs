using System;
using TMPro;
using UnityEngine;

public class DamagePopUpManager : MonoBehaviour
{
    private static event Action<float, bool, Transform> OnDamagePopUpRequested;
    public static void RequestDamagePopUp(float damage, bool isCritical, Transform hit) => OnDamagePopUpRequested?.Invoke(damage, isCritical, hit);

    [SerializeField] private GameObject regularPopUpPrefab = default;
    [SerializeField] private GameObject criticalPopUpPrefab = default;
    [SerializeField] private float displayDuration = 0.5f;

    private void OnEnable() => OnDamagePopUpRequested += InstantiatePopUp;
    private void OnDisable() => OnDamagePopUpRequested -= InstantiatePopUp;

    private void InstantiatePopUp(float damage, bool isCritical, Transform hit) {
        var prefab = isCritical ? criticalPopUpPrefab : regularPopUpPrefab;
        var popup = Instantiate(prefab, transform.position, Quaternion.identity, hit);
        var text = popup.GetComponentInChildren<TextMeshPro>();
        if (text != null)
        {
            text.text = damage.ToString();
        }
        Destroy(popup.gameObject, displayDuration);
    }
}
