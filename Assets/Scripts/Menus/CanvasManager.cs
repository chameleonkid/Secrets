using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private GameObject activeCanvas;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            transform.parent = null;
            Destroy(this.gameObject);
        }
        else {
            Instance = this;
        }
    }

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
