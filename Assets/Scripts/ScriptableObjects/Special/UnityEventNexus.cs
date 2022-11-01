﻿using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Special/Unity Event Nexus")]
public class UnityEventNexus : ScriptableObject
{
    [SerializeField] private ConstrainedFloat mana = default;
    public void ModifyCurrentMana(float deltaMana) => mana.current += (mana.max/100)*deltaMana;

    [SerializeField] private ConstrainedFloat health = default;
    public void ModifyCurrentHealth(float deltaHealth) => health.current += (health.max/100)*deltaHealth;

    [SerializeField] private ConstrainedFloat lumen = default;
    public void ModifyCurrentLumen(float deltaLumen) => lumen.current += (lumen.max/100)*deltaLumen;
    
    public void ShrinkSize()
    {
        var player = FindObjectOfType<PlayerMovement>();
        if (player.transform.localScale.x < 1 && player.transform.localScale.y < 1)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

}
