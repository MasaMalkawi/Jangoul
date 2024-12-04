using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvasStart; // Reference to the Start Canvas
    public GameObject canvasMain;  // Reference to the Main Canvas

    void Start()
    {
        // Ensure only the Start Canvas is active initially
        canvasStart.SetActive(true);
        canvasMain.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        // Switch visibility of canvases
        canvasStart.SetActive(false);
        canvasMain.SetActive(true);
    }
}