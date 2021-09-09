using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Appearance")]
public class CharacterAppearance : ScriptableObject
{
    public Texture2D bodyStyle;
    public Texture2D hairStyle;
    public Texture2D armorStyle;
    public Texture2D eyeColor;
    public Color hairColor;
    public string playerName;

    // Runtime only — not serialized (indexes and paths)
    public int index { get; set; }  //! Currently used for differentiating between male and female skin textures, should rename

    public int bodyIndex { get; set; }
    public int hairIndex { get; set; }
    public int armorIndex { get; set; }
    public int eyeIndex { get; set; }

    public string bodyFolderPath { get; set; }
    public string hairFolderPath { get; set; }
    public string armorFolderPath { get; set; }
    public string eyeFolderPath { get; set; }

#if UNITY_EDITOR    // Reason: paths are normally set when save data is loaded, but this is skipped in editor
    private void OnValidate() => OnEnable();
    private void OnEnable() {
        var path = "Assets/Resources/";
        bodyFolderPath = UnityEditor.AssetDatabase.GetAssetPath(bodyStyle).Replace(path, "");
        bodyFolderPath = bodyFolderPath.Remove(bodyFolderPath.LastIndexOf("/") + 1);
        hairFolderPath = UnityEditor.AssetDatabase.GetAssetPath(hairStyle).Replace(path, "");
        hairFolderPath = hairFolderPath.Remove(hairFolderPath.LastIndexOf("/") + 1);
        armorFolderPath = UnityEditor.AssetDatabase.GetAssetPath(armorStyle).Replace(path, "");
        armorFolderPath = armorFolderPath.Remove(armorFolderPath.LastIndexOf("/") + 1);
        eyeFolderPath = UnityEditor.AssetDatabase.GetAssetPath(eyeColor).Replace(path, "");
        eyeFolderPath = eyeFolderPath.Remove(eyeFolderPath.LastIndexOf("/") + 1);
    }
#endif

    public CharacterAppearanceSerializable GetSerializable() => new CharacterAppearanceSerializable(this);

    public void Deserialize(CharacterAppearanceSerializable cas, SkinTexturesDatabase[] skinTextures)
    {
        playerName = cas.playerName;
        index = cas.index;

        bodyIndex = cas.bodyIndex;
        hairIndex = cas.hairIndex;
        hairColor = cas.hairColor;
        armorIndex = cas.armorIndex;
        eyeIndex = cas.eyeIndex;

        var activeTextures = skinTextures[index];

        bodyStyle = activeTextures.bodySkins[bodyIndex];
        hairStyle = activeTextures.hairStyles[hairIndex];
        armorStyle = activeTextures.armorSkins[armorIndex];
        eyeColor = activeTextures.eyeSkins[eyeIndex];

        bodyFolderPath = activeTextures.bodyFolderPath;
        hairFolderPath = activeTextures.hairFolderPath;
        armorFolderPath = activeTextures.armorFolderPath;
        eyeFolderPath = activeTextures.eyeFolderPath;
    }

    [System.Serializable]
    public struct CharacterAppearanceSerializable
    {
        [ES3Serializable] public string playerName { get; private set; }

        [ES3Serializable] public int index { get; private set; }

        [ES3Serializable] public int bodyIndex { get; private set; }
        [ES3Serializable] public int hairIndex { get; private set; }
        [ES3Serializable] public Color hairColor { get; private set; }
        [ES3Serializable] public int armorIndex { get; private set; }
        [ES3Serializable] public int eyeIndex { get; private set; }

        public CharacterAppearanceSerializable(CharacterAppearance ca)
        {
            playerName = ca.playerName;

            index = ca.index;

            bodyIndex = ca.bodyIndex;
            hairIndex = ca.hairIndex;
            hairColor = ca.hairColor;
            armorIndex = ca.armorIndex;
            eyeIndex = ca.eyeIndex;
        }
    }
}
