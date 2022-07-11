using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private Speaker[] _speakers;
    public Speaker[] speakers => _speakers;

    [TextArea(3, 10)]
    public string[] sentences = new string[1];

    [System.Serializable]
    public class Speaker
    {
        [SerializeField] private string _name;
        public string name => _name;
        [SerializeField] private Color _textboxColor;
        public Color textboxColor => _textboxColor;
    }
}
