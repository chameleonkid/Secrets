using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Single/Unity Event Nexus")]
public class UnityEventNexus : ScriptableObject
{
    [SerializeField] private FloatMeter mana = default;
    public void ModifyCurrentMana(float deltaMana) => mana.current += deltaMana;

    [SerializeField] private FloatMeter health = default;
    public void ModifyCurrentHealth(float deltaHealth) => health.current += deltaHealth;
}
