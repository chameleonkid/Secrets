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

    public int bodyIndex { get; set; }
    public int hairIndex { get; set; }
    public int armorIndex { get; set; }
    public int eyeIndex { get; set; }

    public CharacterAppearanceSerializable GetSerializable() => new CharacterAppearanceSerializable(this);

    public void Deserialize(CharacterAppearanceSerializable cas, SkinTexturesDatabase skinTextures)
    {
        hairColor = cas.hairColor;
        playerName = cas.playerName;

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
        public Color hairColor { get; private set; }
        public string playerName { get; private set; }

        public int bodyIndex { get; private set; }
        public int hairIndex { get; private set; }
        public int armorIndex { get; private set; }
        public int eyeIndex { get; private set; }

        public CharacterAppearanceSerializable(CharacterAppearance ca)
        {
            hairColor = ca.hairColor;
            playerName = ca.playerName;

            bodyIndex = ca.bodyIndex;
            hairIndex = ca.hairIndex;
            armorIndex = ca.armorIndex;
            eyeIndex = ca.eyeIndex;
        }
    }
}
