using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject[] canvases; // Array to hold your canvases

    private int currentCanvasIndex = 0; // Tracks the currently active canvas

    void Start()
    {
        ShowCanvas(currentCanvasIndex); // Show the first canvas at the start
    }

    // Method to show the specified canvas and hide others
    public void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == index); // Show the current canvas, hide others
        }
    }

    // Method to navigate to the next canvas
    public void NextCanvas()
    {
        currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Length; // Loop back to the first canvas if needed
        ShowCanvas(currentCanvasIndex);
    }
}