using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private Vector2 defaultValue = default;
    public Vector2 value;

    public void OnAfterDeserialize()
    {
        value = defaultValue;
    }

    public void OnBeforeSerialize() {}
}

