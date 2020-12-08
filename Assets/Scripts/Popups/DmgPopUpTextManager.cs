using UnityEngine;
using TMPro;

public class DmgPopUpTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro textfield = default;

    private void Start()
    {
        Destroy(this.gameObject, 20f); //or leave this empty and call destroy in your animator
    }

    public void SetText(float damage)
    {
        textfield.text = "" + damage;
        Destroy(this.gameObject, 0.5f);
    }
}
