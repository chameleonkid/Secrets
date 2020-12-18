using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class XPSystem : MonoBehaviour
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    [SerializeField] private int level;
    [SerializeField] private int experience;
    [SerializeField] private int experienceToNextLevel;

    public XPSystem()    //Constructor
    {
        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public void AddExperience( int amount )
    {
        experience += amount;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            if (OnExperienceChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);

    }

    public int GetLevelNumber()
    {
        return level;
    }

    public int GetExperienceNeeded()
    {
        return experienceToNextLevel;
    }

    public int GetExperience()
    {
        return experience;
    }

    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel;
    }

    public void SetExperienceToNextLevel()
    {
        experienceToNextLevel = experienceToNextLevel * 2; 
    }


}
