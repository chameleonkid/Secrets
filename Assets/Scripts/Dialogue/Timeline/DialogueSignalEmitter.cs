using UnityEngine;
using UnityEngine.Timeline;

public class DialogueSignalEmitter : SignalEmitter
{
    [SerializeField] private DialogueSO _parameter;
    public Dialogue parameter => _parameter.value;
}
