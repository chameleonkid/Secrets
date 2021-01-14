using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Primitives/String")]
[System.Serializable]
public class StringValue : ScriptableObject
{
    public string initialValue;
    public string RuntimeValue;
}