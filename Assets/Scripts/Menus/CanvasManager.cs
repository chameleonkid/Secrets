using UnityEngine;

public class CanvasManager : MonoBehaviourSingleton<CanvasManager>
{
    private GameObject activeCanvas;

    public bool IsFreeOrActive(GameObject canvasGameObject)
    {
        if (activeCanvas == canvasGameObject || activeCanvas == null || !activeCanvas.activeInHierarchy)
        {
            activeCanvas = canvasGameObject;
            return true;
        }
        else
        {
            return false;
        }
    }
}
