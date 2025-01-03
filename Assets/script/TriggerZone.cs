/*using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public AudioSource guideAudio;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger zone and audio is playing.");

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
}*/




using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public AudioSource guideAudio;
    public FireworkManager fireworkManager;  // Reference to FireworkManager

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger zone and audio is playing.");

        if (other.CompareTag("Player"))
        {
            // Play the guide audio if it's not already playing
            if (guideAudio != null && !guideAudio.isPlaying)
            {
                guideAudio.Play();
                Debug.Log("Guide audio is playing.");
            }

            // Trigger firework
            if (fireworkManager != null)
            {
                fireworkManager.TriggerFirework();  // Trigger the firework effect
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the guide audio when the player exits
            if (guideAudio != null)
            {
                guideAudio.Stop();
                Debug.Log("Guide audio stopped.");
            }
        }
    }
}



