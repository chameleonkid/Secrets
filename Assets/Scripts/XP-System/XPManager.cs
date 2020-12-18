using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class XPManager : MonoBehaviour
{
    [SerializeField] private XPWindow xpWindow = default;
    [SerializeField] private XPSystem levelSystem = default;

    private void Awake()
    {
        xpWindow.SetLevelSystem(levelSystem);
    }

}
