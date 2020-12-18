using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    [SerializeField] private XPWindow xpWindow;
    [SerializeField] private XPSystem levelSystem;

    private void Awake()
    {
        Debug.Log(levelSystem.GetLevelNumber());
        levelSystem.AddExperience(50);
        Debug.Log(levelSystem.GetLevelNumber());
        levelSystem.AddExperience(60);
        Debug.Log(levelSystem.GetLevelNumber());


        xpWindow.SetLevelSystem(levelSystem);
    }

}
