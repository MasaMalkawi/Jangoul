using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public AudioSource guideAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && guideAudio != null)
        {
            if (!guideAudio.isPlaying)
            {
                guideAudio.Play();
                Debug.Log("Player entered the trigger zone and audio is playing.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && guideAudio != null)
        {
            guideAudio.Stop();
            Debug.Log("Player exited the trigger zone and audio stopped.");
        }
    }
}


