using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FruitAndVegetableManager : MonoBehaviour
{
    public string objectType; // Can be "Fruit" or "Vegetable"
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Vector3 originalPosition; // To store the original position
    private Quaternion originalRotation; // To store the original rotation
    public AudioClip successSound; // Sound for placing in the correct basket
    public AudioClip errorSound;   // Sound for placing in the wrong basket
    private AudioSource audioSource;

    void Start()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable is missing on this object!");
            return;
        }

        // Store the original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Disable physics initially
        rb.isKinematic = true;
        rb.useGravity = false;

        // Add listeners for grab events programmatically
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // Add AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Debug.Log("FruitAndVegetableManager initialized and event listeners added.");
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Enable physics when grabbed
        rb.isKinematic = false;
        rb.useGravity = true;

        Debug.Log($"Object grabbed: {gameObject.name}");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"Object released: {gameObject.name}");

        // Check if the object is placed correctly or dropped on the ground
        Invoke(nameof(CheckPosition), 0.1f); // Slight delay to ensure physics settle
    }

    private void CheckPosition()
    {
        // Check for the "Ground" directly below the object
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Object is on the ground. It can be picked up again.");
                return; // Exit the function if the object is on the ground
            }
        }

        // If not on the ground, check if it's in the correct basket
        CheckBasket();
    }
    

    

    private void CheckBasket()
    {
        // Check if the object is placed in the correct basket
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f); // Check nearby colliders within a small radius
        bool isCorrectBasket = false; // Track if the object is in the correct basket

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("FruitBasket") && objectType == "Fruit")
            {
                // Fruit placed in the fruit basket
                Debug.Log("Fruit placed in the correct basket.");
                PlaySuccessSound(); // Play success sound
                isCorrectBasket = true;
                break;
            }
            else if (collider.CompareTag("VegetableBasket") && objectType == "Vegetable")
            {
                // Vegetable placed in the vegetable basket
                Debug.Log("Vegetable placed in the correct basket.");
                PlaySuccessSound(); // Play success sound
                isCorrectBasket = true;
                break;
            }
        }

        // If not placed in the correct basket, reset its position and play the error sound
        if (!isCorrectBasket)
        {
            Debug.Log("Object placed in the wrong basket. Returning it to its original position.");
            PlayErrorSound(); // Play error sound
            ReturnToOriginalPosition();
        }
    }

    private void PlaySuccessSound()
    {
        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound); // Play success sound
        }
    }

    private void PlayErrorSound()
    {
        if (audioSource != null && errorSound != null)
        {
            audioSource.PlayOneShot(errorSound); // Play error sound
        }
    }

    private void ReturnToOriginalPosition()
    {
        // Reset the object to its original position
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    private void OnDestroy()
    {
        // Remove listeners to prevent memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }
}