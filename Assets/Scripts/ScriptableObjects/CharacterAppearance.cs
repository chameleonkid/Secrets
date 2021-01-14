using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/Appearance")]
[System.Serializable]
public class CharacterAppearance : ScriptableObject
{
    public Texture2D bodyStyle;
    public Texture2D hairStyle;
    public Color hairColor;
    public Texture2D armorStyle;
    public Texture2D eyeColor;
    public string playerName;
}