
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject[] canvases; // Array to hold your canvases
    private ObjectManager objectManager; // Reference to the ObjectManager

    private int currentCanvasIndex = 0; // Tracks the currently active canvas

    void Start()
    {
        // Find the ObjectManager in the scene
        objectManager = FindObjectOfType<ObjectManager>();

        // Show the first canvas and notify ObjectManager
        ShowCanvas(currentCanvasIndex);
    }

    // Method to show the specified canvas and hide others
    public void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == index); // Show the specified canvas, hide others
        }

        // Notify the ObjectManager to show the associated objects
        if (objectManager != null)
        {
            objectManager.ShowObjects(index); // Use ShowObjects instead of ShowObject
        }
        else
        {
            Debug.LogWarning("ObjectManager not found in the scene!");
        }
    }

    // Method to navigate to the next canvas
    public void NextCanvas()
    {
        // Deactivate the current canvas
        canvases[currentCanvasIndex].SetActive(false);

        // Move to the next canvas in order
        currentCanvasIndex++;

        // Reset to the first canvas if at the end
        if (currentCanvasIndex >= canvases.Length)
        {
            currentCanvasIndex = 0;
        }

        // Show the next canvas
        ShowCanvas(currentCanvasIndex);
    }
}