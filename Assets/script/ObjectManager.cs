using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] canvasObjectGroups;

    public void ShowObjects(int canvasIndex)
    {
        Debug.Log($"Switching to canvas {canvasIndex}");

        for (int i = 0; i < canvasObjectGroups.Length; i++)
        {
            bool shouldActivate = i == canvasIndex;
            canvasObjectGroups[i].SetActive(shouldActivate);
            Debug.Log($"Canvas Object Group {i} active: {shouldActivate}");
        }
    }
}

