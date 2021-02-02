using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Primitives/Bool")]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    public bool initialValue;

    // [HideInInspector]
    public bool RuntimeValue;

    /*
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {

    }
    */
}
