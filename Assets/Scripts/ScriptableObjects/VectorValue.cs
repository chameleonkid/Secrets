using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Primitives/Vector2")]
public class VectorValue : ScriptableObject
{
    public Vector2 defaultValue = default;
    public Vector2 value;

    public void ResetValue()
    {
        value = defaultValue;
    }

}

