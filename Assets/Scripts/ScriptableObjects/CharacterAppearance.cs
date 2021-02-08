using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Appearance")]
public class CharacterAppearance : ScriptableObject
{
    public Texture2D bodyStyle;
    public Texture2D hairStyle;
    public Color hairColor;
    public Texture2D armorStyle;
    public Texture2D eyeColor;
    public string playerName;
    public bool isFemale = false;

    public int bodyIndex { get; set; }
    public int hairIndex { get; set; }
    public int armorIndex { get; set; }
    public int eyeIndex { get; set; }

    public CharacterAppearanceSerializable GetSerializable() => new CharacterAppearanceSerializable(this);

    public void Deserialize(CharacterAppearanceSerializable cas, SkinTexturesDatabase skinTextures)
    {
        hairColor = cas.hairColor;
        playerName = cas.playerName;
        isFemale = cas.isFemale;

        bodyIndex = cas.bodyIndex;
        hairIndex = cas.hairIndex;
        armorIndex = cas.armorIndex;
        eyeIndex = cas.eyeIndex;

        bodyStyle = skinTextures.bodySkins[bodyIndex];
        hairStyle = skinTextures.hairStyles[hairIndex];
        armorStyle = skinTextures.armorSkins[armorIndex];
        eyeColor = skinTextures.eyeSkins[eyeIndex];
    }

    [System.Serializable]
    public struct CharacterAppearanceSerializable
    {
        [ES3Serializable] public Color hairColor { get; private set; }
        [ES3Serializable] public string playerName { get; private set; }
        [ES3Serializable] public bool isFemale { get; private set; }

        [ES3Serializable] public int bodyIndex { get; private set; }
        [ES3Serializable] public int hairIndex { get; private set; }
        [ES3Serializable] public int armorIndex { get; private set; }
        [ES3Serializable] public int eyeIndex { get; private set; }

        public CharacterAppearanceSerializable(CharacterAppearance ca)
        {
            hairColor = ca.hairColor;
            playerName = ca.playerName;

            bodyIndex = ca.bodyIndex;
            hairIndex = ca.hairIndex;
            armorIndex = ca.armorIndex;
            eyeIndex = ca.eyeIndex;
            isFemale = ca.isFemale;
        }
    }
}
