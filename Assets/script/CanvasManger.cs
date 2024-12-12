using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject[] canvases;
    private ObjectManager objectManager;

    private int currentCanvasIndex = 0;

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        ShowCanvas(currentCanvasIndex);
    }

    public void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == index);
        }

        if (objectManager != null)
        {
            objectManager.ShowObjects(index);
        }
    }

    public void NextCanvas()
    {
        currentCanvasIndex = (currentCanvasIndex + 1) % canvases.Length;
        ShowCanvas(currentCanvasIndex);
    }
}