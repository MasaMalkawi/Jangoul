using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] canvasObjectGroups; // Array of object groups corresponding to each canvas

    // Method to show objects associated with the active canvas
    public void ShowObjects(int canvasIndex)
    {
        Debug.Log($"Activating objects for canvas index: {canvasIndex}");
        // Loop through all canvas object groups
        for (int i = 0; i < canvasObjectGroups.Length; i++)
        {
            // Activate all objects related to the canvas index
            // This assumes that each element in canvasObjectGroups contains all objects for that canvas
            canvasObjectGroups[i].SetActive(i == canvasIndex); // Show only the objects for the active canvas
            Debug.Log($"Object group {i} active: {canvasObjectGroups[i].activeSelf}");
        }
    }
}