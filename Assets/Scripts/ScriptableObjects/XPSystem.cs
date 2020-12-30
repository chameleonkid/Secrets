using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/XP System")]
public class XPSystem : ScriptableObject
{
    public event Action OnExperienceChanged;
    public event Action OnLevelChanged;

    [SerializeField] private int _level = default;
    public int level => _level;
    [SerializeField] private int _experience = default;
    public int experience => _experience;
    [SerializeField] private int _experienceToNextLevel = default;
    public int experienceToNextLevel => _experienceToNextLevel;

    public float GetExperienceNormalized() => (float)experience / experienceToNextLevel;

    public void AddExperience(int amount)
    {
        _experience += amount;
        while (experience >= experienceToNextLevel)
        {
            _level++;
            _experience -= experienceToNextLevel;
            _experienceToNextLevel *= 2;
            OnLevelChanged?.Invoke();
        }
        OnExperienceChanged?.Invoke();
    }

    public void ResetExperienceSystem()
    {
        _level = 1;
        _experience = 0;
        _experienceToNextLevel = 100;
    }
}
