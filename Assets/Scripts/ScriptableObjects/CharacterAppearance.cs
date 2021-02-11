using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Appearance")]
public class CharacterAppearance : ScriptableObject
{
    public Texture2D bodyStyle;
    public Texture2D hairStyle;
    public Texture2D armorStyle;
    public Texture2D eyeColor;

    public string playerName;   // Should probably make into property with private backing field exposed to Inspector

    public int index { get; set; }

    public int bodyIndex { get; set; }
    public int hairIndex { get; set; }
    public Color hairColor;     // Should probably make into property with private backing field exposed to Inspector
    public int armorIndex { get; set; }
    public int eyeIndex { get; set; }

    // Runtime only
    public string bodyFolderPath { get; set; }
    public string hairFolderPath { get; set; }
    public string armorFolderPath { get; set; }
    public string eyeFolderPath { get; set; }

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
