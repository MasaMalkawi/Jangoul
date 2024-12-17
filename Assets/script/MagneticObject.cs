using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    [Header("Magnetic Settings")]
    [SerializeField] private GameObject targetCanvas;    // The canvas or target area to magnetize towards
    [SerializeField] private float magneticRange = 1.0f; // Range within which magnetism applies
    [SerializeField] private float snapSpeed = 5f;       // Speed at which the object is pulled towards the target

    private Rigidbody rb;       // Rigidbody of the object
    private bool isHeld = false; // Tracks if the player is holding the object
    private bool isSnapped = false; // To check if it's snapped

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found! Please add a Rigidbody to the object.");
        }
    }

    void Update()
    {
        if (!isHeld && !isSnapped && targetCanvas != null)
        {
            // Calculate the distance to the target canvas
            float distanceToCanvas = Vector3.Distance(transform.position, targetCanvas.transform.position);

            // If within magnetic range, start pulling the object towards the canvas
            if (distanceToCanvas <= magneticRange)
            {
                MagnetizeToCanvas();
            }
        }
    }

    /// <summary>
    /// Magnetizes (smoothly pulls) the object towards the target canvas.
    /// </summary>
    private void MagnetizeToCanvas()
    {
        Debug.Log("Object being magnetized towards the target...");

        // Get the direction towards the target canvas
        Vector3 direction = (targetCanvas.transform.position - transform.position).normalized;

        // Move the object smoothly towards the target canvas
        rb.MovePosition(Vector3.Lerp(transform.position, targetCanvas.transform.position, snapSpeed * Time.deltaTime));

        // Convert world position to canvas local position
        Vector3 localPositionOnCanvas = targetCanvas.transform.InverseTransformPoint(transform.position);

        // If the object is close enough, snap it to the canvas
        if (Vector3.Distance(localPositionOnCanvas, Vector3.zero) <= 0.1f)
        {
            SnapToCanvas(localPositionOnCanvas);
        }
    }

    /// <summary>
    /// Snaps the object to the target canvas at its relative position.
    /// </summary>
    private void SnapToCanvas(Vector3 relativePosition)
    {
        Debug.Log("Object snapped to the canvas!");

        // Snap the object relative to the canvas at its current position
        transform.SetParent(targetCanvas.transform, true);

        // Set the object's local position to its position on the canvas
        transform.localPosition = relativePosition;
        transform.localRotation = Quaternion.identity; // Optionally reset rotation

        // Lock the object at the canvas position
        rb.isKinematic = true;   // Disable physics after snap
        isSnapped = true;        // Mark as snapped
    }

    /// <summary>
    /// Call this method when the player grabs the object.
    /// </summary>
    public void OnGrab()
    {
        Debug.Log("Object grabbed!");
        isHeld = true;           // Object is now held
        rb.isKinematic = true;   // Disable physics while held
    }

    /// <summary>
    /// Call this method when the player releases the object.
    /// </summary>
    public void OnRelease()
    {
        Debug.Log("Object released!");
        isHeld = false;          // Object is no longer held
        rb.isKinematic = false;  // Re-enable physics
        isSnapped = false;       // Reset snap state

        // If close enough to the canvas, snap it
        float distanceToCanvas = Vector3.Distance(transform.position, targetCanvas.transform.position);
        if (distanceToCanvas <= magneticRange)
        {
            Vector3 localPositionOnCanvas = targetCanvas.transform.InverseTransformPoint(transform.position);
            SnapToCanvas(localPositionOnCanvas);
        }
    }
}