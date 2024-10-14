using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioClip guideSound;  // الصوت الذي تريد تشغيله
    private AudioSource audioSource;

    void Start()
    {
        // إعداد AudioSource لتشغيل الصوت
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = guideSound;
    }

    // يتفعل عند دخول اللاعب في المنطقة الوهمية
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Controller entered the zone!");  // سيظهر هذا في الـConsole عند دخول الـController
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}

