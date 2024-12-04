using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    public Canvas firstCanvas;  // Reference to the first canvas
    public Canvas secondCanvas; // Reference to the second canvas
    public Button switchButton; // Reference to the button that switches canvases

    void Start()
    {
        // Ensure that the second canvas is initially disabled
        secondCanvas.gameObject.SetActive(false);

        // Add listener to the button's onClick event
        switchButton.onClick.AddListener(SwitchCanvas);
    }

    void SwitchCanvas()
    {
        // Disable the first canvas and enable the second one
        firstCanvas.gameObject.SetActive(false);
        secondCanvas.gameObject.SetActive(true);
    }
}