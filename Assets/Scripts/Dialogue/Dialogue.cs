using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private Speaker[] _speakers = new Speaker[1];
    public Speaker[] speakers => _speakers;

    [SerializeField] private Line[] _lines = new Line[1];
    public Line[] lines => _lines;

    [System.Serializable]
    public class Speaker
    {
        [SerializeField] private string _name;
        public string name => _name;
        [SerializeField] private Color _textboxColor;
        public Color textboxColor => _textboxColor;
    }

    public class Line
    {
        [SerializeField] private int _speakerIndex;
        public int speakerIndex => _speakerIndex;
        [TextArea(3, 10)]
        public string text;
    }
}
