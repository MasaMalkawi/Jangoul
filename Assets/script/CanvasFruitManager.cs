using UnityEngine;
using UnityEngine.UI;

public class CanvasFruitManager : MonoBehaviour
{
    // References to your canvases
    public GameObject mainCanvas;
    public GameObject fruitsCanvas;
    public GameObject vegetablesCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Initially, show only the Main Canvas
        ShowMainCanvas();
    }

    // Function to show Main Canvas
    public void ShowMainCanvas()
    {
        mainCanvas.SetActive(true);
        fruitsCanvas.SetActive(false);
        vegetablesCanvas.SetActive(false);
    }

    // Function to show Fruits Canvas
    public void ShowFruitsCanvas()
    {
        mainCanvas.SetActive(false);
        fruitsCanvas.SetActive(true);
        vegetablesCanvas.SetActive(false);
    }

    // Function to show Vegetables Canvas
    public void ShowVegetablesCanvas()
    {
        mainCanvas.SetActive(false);
        fruitsCanvas.SetActive(false);
        vegetablesCanvas.SetActive(true);
    }
}