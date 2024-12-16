using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Canvas Management")]
    [SerializeField] private GameObject[] canvases; // Canvases to manage
    private int currentCanvasIndex = 0; // Tracks the active canvas index

    void Start()
    {
        // Initialize canvas
        ShowCanvas(currentCanvasIndex);
    }

    // Show a specific canvas by index
    public void ShowCanvas(int index)
    {
        if (index < 0 || index >= canvases.Length)
        {
            Debug.LogError("Invalid canvas index: " + index);
            return;
        }

        // Deactivate all canvases, then activate the selected one
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == index);
        }
    }

    // Switch to the next canvas
    public void NextCanvas()
    {
        currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Length;
        ShowCanvas(currentCanvasIndex);
    }
}