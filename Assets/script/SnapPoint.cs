using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public Transform snapPosition; // The position and rotation where objects will snap

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a tag or component to identify it
        if (other.CompareTag("Grabbable")) // Ensure objects have the "Grabbable" tag
        {
            // Snap the object to the snap point
            other.transform.position = snapPosition.position;
            other.transform.rotation = snapPosition.rotation;

            // Optional: Disable Rigidbody for precise snapping
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Re-enable Rigidbody when the object leaves the snap point
        if (other.CompareTag("Grabbable"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
}