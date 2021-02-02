using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Special/Skin Textures Database")]
public class SkinTexturesDatabase : ScriptableObject
{
    [SerializeField] private Texture2D[] _bodySkins = default;
    public Texture2D[] bodySkins => _bodySkins;

    [SerializeField] private Texture2D[] _hairStyles = default;
    public Texture2D[] hairStyles => _hairStyles;

    [SerializeField] private Texture2D[] _armorSkins = default;
    public Texture2D[] armorSkins => _armorSkins;

    [SerializeField] private Texture2D[] _eyeSkins = default;
    public Texture2D[] eyeSkins => _eyeSkins;
}
