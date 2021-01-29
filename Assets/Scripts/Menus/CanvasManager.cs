using Schwer;
using UnityEngine;

public class CanvasManager : MonoBehaviourSingleton<CanvasManager>
{
    private GameObject activeCanvas;

    private GameObject previousCanvas;
    private int previousCanvasFrame;

    public bool IsFreeOrActive(GameObject canvasGameObject)
    {
        if (IsSameOrCanReplace(canvasGameObject) && NotReplacingSameFrame(canvasGameObject))
        {
            previousCanvasFrame = Time.frameCount;
            previousCanvas = activeCanvas;
            activeCanvas = canvasGameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsSameOrCanReplace(GameObject newCanvas) => activeCanvas == newCanvas || activeCanvas == null || !activeCanvas.activeInHierarchy;
    private bool NotReplacingSameFrame(GameObject newCanvas) => previousCanvas == null || !(previousCanvas == newCanvas && Time.frameCount == previousCanvasFrame);
}
