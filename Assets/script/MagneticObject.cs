using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas; // The canvas this object snaps to
    [SerializeField] private float snapDistance = 0.5f; // Distance at which snapping happens
    [SerializeField] private Transform snapPoint; // Optional: Specific point on the canvas to snap to

    private bool isHeld = false; // Tracks if the player is holding the object
    private Rigidbody rb; // Rigidbody of the object

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
        // Only perform snapping logic if the object is not being held
        if (!isHeld && targetCanvas != null)
        {
            // Check if the object is close enough to snap
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
        rb.isKinematic = true; // Disable physics while held
    }

    // Call this method when the player releases the object
    public void OnRelease()
    {
        Debug.Log("Object released!");
        isHeld = false; // Object is no longer held
        rb.isKinematic = false; // Re-enable physics

        // Snap the object to the canvas based on its current position relative to the canvas
        SnapToCanvas();
    }

    // Snaps the object to the target canvas
    private void SnapToCanvas()
    {
        // Calculate the position on the canvas where the object is released
        Vector3 canvasPosition = targetCanvas.transform.InverseTransformPoint(transform.position);

        // Set the object's local position relative to the canvas
        transform.SetParent(targetCanvas.transform);
        transform.localPosition = new Vector3(canvasPosition.x, canvasPosition.y, 0); // Set Z to 0 or adjust as needed
        transform.localRotation = Quaternion.identity; // Reset rotation or set to desired rotation

        // Lock the object in place after snapping
        rb.isKinematic = true;
    }
}