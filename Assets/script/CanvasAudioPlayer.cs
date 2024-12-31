using UnityEngine;

public class CanvasAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Drag the Audio Source here in the Inspector
    public AudioClip voiceClip;    // Drag the voice clip here in the Inspector

    private void OnEnable()
    {
        if (audioSource != null && voiceClip != null)
        {
            audioSource.PlayOneShot(voiceClip);
        }
        else
        {
            Debug.LogWarning("AudioSource or VoiceClip is not assigned.");
        }
    }
}
