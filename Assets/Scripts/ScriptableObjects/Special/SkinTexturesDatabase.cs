using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Special/Skin Textures Database")]
public class SkinTexturesDatabase : ScriptableObject
{
    [Header("Folder Paths")]
    [SerializeField] private string _bodyFolderPath = default;
    public string bodyFolderPath => _bodyFolderPath;

    [SerializeField] private string _hairFolderPath = default;
    public string hairFolderPath => _hairFolderPath;
    
    [SerializeField] private string _armorFolderPath = default;
    public string armorFolderPath => _armorFolderPath;

    [SerializeField] private string _eyeFolderPath = default;
    public string eyeFolderPath => _eyeFolderPath;

    [Header("Sprites")]
    [SerializeField] private Texture2D[] _bodySkins = default;
    public Texture2D[] bodySkins => _bodySkins;

    [SerializeField] private Texture2D[] _hairStyles = default;
    public Texture2D[] hairStyles => _hairStyles;

    [SerializeField] private Texture2D[] _armorSkins = default;
    public Texture2D[] armorSkins => _armorSkins;

    [SerializeField] private Texture2D[] _eyeSkins = default;
    public Texture2D[] eyeSkins => _eyeSkins;
}
