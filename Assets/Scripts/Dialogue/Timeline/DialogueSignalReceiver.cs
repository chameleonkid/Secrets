using UnityEngine;
using UnityEngine.Playables;

public class DialogueSignalReceiver : MonoBehaviour, INotificationReceiver
{
    // https://gametorrahod.com/how-to-make-a-custom-signal-receiver-with-emitter-parameter/
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is DialogueSignalEmitter e)
        {
            DialogueManager.RequestDialogue(e.parameter);
        }
    }
}
