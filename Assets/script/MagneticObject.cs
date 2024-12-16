using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas; // The canvas this object snaps to
    [SerializeField] private float snapDistance = 0.5f; // Distance at which snapping happens
    [SerializeField] private Transform snapPoint; // Optional: Specific point on the canvas to snap to

    private bool isHeld = false; // Tracks if the player is holding the object
    private Rigidbody rb; // Rigidbody of the object
    private bool isSnapped = false; // Tracks if the object is snapped

    public bool IsSnapped => isSnapped; // Public read-only property to check snapped state

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on the object. Please add one!");
        }
    }

    void Update()
    {
        // Only perform snapping logic if the object is not being held and not already snapped
        if (!isHeld && !isSnapped && targetCanvas != null)
        {
            float distanceToCanvas = Vector3.Distance(transform.position, targetCanvas.transform.position);

            if (distanceToCanvas <= snapDistance)
            {
                SnapToCanvas();
            }
        }
    }

    // Call this method when the player grabs the object
    public void OnGrab()
    {
        Debug.Log("Object grabbed!");
        isHeld = true; // Object is now held
        isSnapped = false; // Mark as unsnapped
        rb.isKinematic = true; // Disable physics while held
        transform.SetParent(null); // Detach from parent while being held
    }

    // Call this method when the player releases the object
    public void OnRelease()
    {
        Debug.Log("Object released!");
        isHeld = false; // Object is no longer held
        rb.isKinematic = false; // Re-enable physics

        // Check for snapping after release
        if (targetCanvas != null)
        {
            float distanceToCanvas = Vector3.Distance(transform.position, targetCanvas.transform.position);

            if (distanceToCanvas <= snapDistance)
            {
                SnapToCanvas();
            }
        }
    }

    // Snaps the object to the target canvas
    private void SnapToCanvas()
    {
        Debug.Log("Object snapped to canvas!");

        if (snapPoint != null)
        {
            // Snap to a specific point if provided
            transform.position = snapPoint.position;
            transform.rotation = snapPoint.rotation;
        }
        else
        {
            // Calculate position relative to the canvas
            Vector3 canvasPosition = targetCanvas.transform.InverseTransformPoint(transform.position);

            // Set local position and rotation on the canvas
            transform.SetParent(targetCanvas.transform);
            transform.localPosition = new Vector3(canvasPosition.x, canvasPosition.y, 0); // Adjust Z as needed
            transform.localRotation = Quaternion.identity; // Reset rotation or set to desired rotation
        }

        // Lock the object in place after snapping
        rb.isKinematic = true;
        isSnapped = true;
    }

    // Unsnap the object, allowing it to be moved again
    public void Unsnap()
    {
        Debug.Log("Object unsnapped!");
        isSnapped = false;
        rb.isKinematic = false;
        transform.SetParent(null); // Detach from parent
    }

    private void OnTriggerEnter(Collider other)
    {
        // Optional: Automatically snap if entering a specific zone
        if (!isHeld && other.gameObject == targetCanvas)
        {
            SnapToCanvas();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Unsnap if leaving the snap zone
        if (isSnapped && other.gameObject == targetCanvas)
        {
            Unsnap();
        }
    }
}