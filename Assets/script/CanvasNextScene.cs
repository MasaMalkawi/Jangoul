using UnityEngine;

public class CanvasNextScene : MonoBehaviour
{
    // References to the canvases
    public GameObject currentCanvas; // The canvas that is currently active
    public GameObject nextCanvas;    // The canvas you want to show next

    // Function to switch to the next canvas
    public void ShowNextCanvas()
    {
        currentCanvas.SetActive(false); // Hide the current canvas
        nextCanvas.SetActive(true);     // Show the next canvas
    }
}
