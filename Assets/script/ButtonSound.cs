using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip sound; // Assign the sound in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound()
    {
        // Play the assigned sound
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}