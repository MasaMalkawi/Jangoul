/*using UnityEngine;

public class CanvasObjectManager : MonoBehaviour
{
    public Canvas targetCanvas;       // Reference to the canvas
    public GameObject[] groupedObjects; // Objects to show/hide

    private void Update()
    {
        if (targetCanvas == null)
        {
            Debug.LogError("No canvas assigned to targetCanvas!");
            return;
        }

        bool isCanvasActive = targetCanvas.gameObject.activeInHierarchy;
        Debug.Log("Canvas Active: " + isCanvasActive);

        foreach (GameObject obj in groupedObjects)
        {
            if (obj != null)
            {
                Debug.Log($"Setting {obj.name} Active: {isCanvasActive}");
                obj.SetActive(isCanvasActive); // Enable/Disable objects
            }
            else
            {
                Debug.LogWarning("Null object in groupedObjects array!");
            }
        }
    }
}*/


using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CanvasObjectGroup
{
    public Canvas targetCanvas;       // Reference to the canvas
    public GameObject[] groupedObjects; // Objects to show/hide for this canvas
}

public class CanvasObjectManager : MonoBehaviour
{
    public List<CanvasObjectGroup> canvasObjectGroups; // List of canvas-object group pairs

    private int activeCanvasIndex = -1;   // Tracks the currently active canvas

    private void Update()
    {
        for (int i = 0; i < canvasObjectGroups.Count; i++)
        {
            CanvasObjectGroup group = canvasObjectGroups[i];

            if (group.targetCanvas == null)
            {
                Debug.LogError($"Canvas at index {i} is not assigned!");
                continue;
            }

            bool isCanvasActive = group.targetCanvas.gameObject.activeInHierarchy;

            // Activate objects for the active canvas and deactivate others
            if (isCanvasActive && activeCanvasIndex != i)
            {
                Debug.Log($"Activating canvas {i} and hiding others.");
                ShowObjectsForCanvas(i);
                activeCanvasIndex = i;
            }
        }
    }

    private void ShowObjectsForCanvas(int canvasIndex)
    {
        for (int i = 0; i < canvasObjectGroups.Count; i++)
        {
            CanvasObjectGroup group = canvasObjectGroups[i];

            if (group.groupedObjects == null || group.groupedObjects.Length == 0)
            {
                Debug.LogWarning($"No grouped objects assigned for canvas {i}!");
                continue;
            }

            bool isActive = i == canvasIndex;

            foreach (GameObject obj in group.groupedObjects)
            {
                if (obj != null)
                {
                    Debug.Log($"Setting {obj.name} Active: {isActive}");
                    obj.SetActive(isActive);
                }
                else
                {
                    Debug.LogWarning("Null object in groupedObjects array!");
                }
            }
        }
    }
}