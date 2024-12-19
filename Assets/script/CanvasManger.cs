using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField] private GameObject[] canvases;

    private int currentCanvasIndex = 0;

    void Start()
    {
        ShowCanvas(currentCanvasIndex);
    }

    public void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == index);
        }

        // Get the SnapPoint of the active canvas
        SnapPoint snapPoint = canvases[index].GetComponentInChildren<SnapPoint>();
        if (snapPoint != null && snapPoint.totalObjectsToSnap == snapPoint.snappedObjectCount)
        {
            snapPoint.TriggerFireworks();
        }
    }

    public void NextCanvas()
    {
        canvases[currentCanvasIndex].SetActive(false);
        currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Length;
        ShowCanvas(currentCanvasIndex);
    }
}


/*using UnityEngine;

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

        // Trigger fireworks on Canvas 1 to 6 only (index 1 to 6)
        if (index > 0 && index < canvases.Length)  // Exclude Canvas 0
        {
            SnapPoint snapPoint = canvases[index].GetComponentInChildren<SnapPoint>();
            if (snapPoint != null && snapPoint.snappedObjectCount >= snapPoint.totalObjectsToSnap)
            {
                snapPoint.TriggerFireworks();
            }
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
}*/




/*using UnityEngine;

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

        // Trigger fireworks only if all objects are snapped and the canvas is visible
        SnapPoint snapPoint = canvases[index].GetComponentInChildren<SnapPoint>(); // Assuming SnapPoint is a child of the canvas
        if (snapPoint != null && snapPoint.snappedObjectCount >= snapPoint.totalObjectsToSnap)
        {
            snapPoint.TriggerFireworks();
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
}*/

/*using UnityEngine;

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
        // Loop through canvases and activate the selected one
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

        // Trigger fireworks when canvas appears and all objects are snapped
        TriggerFireworksOnCanvas(index);
    }

    // Method to trigger fireworks on the given canvas if all objects are snapped
    private void TriggerFireworksOnCanvas(int index)
    {
        // Get the SnapPoint components in the current canvas
        SnapPoint[] snapPoints = canvases[index].GetComponentsInChildren<SnapPoint>();

        // Check if all objects in the canvas are snapped
        foreach (SnapPoint snapPoint in snapPoints)
        {
            if (snapPoint.snappedObjectCount < snapPoint.totalObjectsToSnap)
            {
                return; // If any object isn't snapped, return without triggering fireworks
            }
        }

        // If all objects are snapped, trigger fireworks for all snap points in the canvas
        foreach (SnapPoint snapPoint in snapPoints)
        {
            snapPoint.TriggerFireworks();
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
}*/