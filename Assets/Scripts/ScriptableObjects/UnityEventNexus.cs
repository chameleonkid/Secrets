using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Special/Unity Event Nexus")]
public class UnityEventNexus : ScriptableObject
{
    [SerializeField] private ConstrainedFloat mana = default;
    public void ModifyCurrentMana(float deltaMana) => mana.current += deltaMana;

    [SerializeField] private ConstrainedFloat health = default;
    public void ModifyCurrentHealth(float deltaHealth) => health.current += deltaHealth;
}
