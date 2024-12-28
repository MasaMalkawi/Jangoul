using UnityEngine;

public class CanvasSwitcherWithSound : MonoBehaviour
{
    // References to the canvases
    public GameObject currentCanvas; // The canvas that is currently active
    public GameObject nextCanvas;    // The canvas you want to show next

    // Reference to the audio source for playing sounds
    public AudioSource audioSource;  // The audio source component
    public AudioClip switchSound;    // The sound to play when switching canvases

    // Function to switch to the next canvas
    public void ShowNextCanvas()
    {
        // Play the switch sound
        if (audioSource != null && switchSound != null)
        {
            audioSource.PlayOneShot(switchSound);
        }

        // Hide the current canvas
        currentCanvas.SetActive(false);

        // Show the next canvas
        nextCanvas.SetActive(true);
    }
}

