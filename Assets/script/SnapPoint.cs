using System.Collections;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [Header("Snap Settings")]
    public Transform snapPosition; // Target position where the object snaps
    public float snapDistance = 0.5f; // Distance threshold to allow snapping
    public float snapDuration = 0.2f; // Duration for smooth snapping

    private bool isSnapped = false; // Whether an object is currently snapped
    private GameObject currentSnappedObject; // The currently snapped object

    void Update()
    {
        // Check if an object is being held and close enough to snap
        if (currentSnappedObject == null || isSnapped) return;

        float distance = Vector3.Distance(currentSnappedObject.transform.position, snapPosition.position);
        if (distance <= snapDistance)
        {
            StartCoroutine(SmoothSnap(currentSnappedObject));
        }
    }

    /// <summary>
    /// Call this method when the object is grabbed.
    /// </summary>
    public void OnGrab(GameObject obj)
    {
        Debug.Log("Object grabbed!");
        if (currentSnappedObject == obj)
        {
            Unsnap(); // Allow unsnapping when grabbed
        }

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        isSnapped = false;
        currentSnappedObject = obj;
        obj.transform.SetParent(null);
    }

    /// <summary>
    /// Call this method when the object is released.
    /// </summary>
    public void OnRelease(GameObject obj)
    {
        Debug.Log("Object released!");
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Re-enable physics
        }

        float distance = Vector3.Distance(obj.transform.position, snapPosition.position);
        if (distance <= snapDistance)
        {
            StartCoroutine(SmoothSnap(obj));
        }
    }

    /// <summary>
    /// Smoothly snaps the object to the snap position.
    /// </summary>
    private IEnumerator SmoothSnap(GameObject obj)
    {
        Debug.Log("Smooth snapping started...");
        isSnapped = true;
        currentSnappedObject = obj;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Vector3 startPosition = obj.transform.position;
        Quaternion startRotation = obj.transform.rotation;

        float elapsed = 0f;

        while (elapsed < snapDuration)
        {
            elapsed += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPosition, snapPosition.position, elapsed / snapDuration);
            obj.transform.rotation = Quaternion.Slerp(startRotation, snapPosition.rotation, elapsed / snapDuration);
            yield return null;
        }

        obj.transform.position = snapPosition.position;
        obj.transform.rotation = snapPosition.rotation;

        Debug.Log("Object snapped!");
    }

    /// <summary>
    /// Unsnap the currently snapped object.
    /// </summary>
    public void Unsnap()
    {
        Debug.Log("Object unsnapped!");

        if (currentSnappedObject != null)
        {
            Rigidbody rb = currentSnappedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Allow free movement again
            }

            currentSnappedObject = null;
        }

        isSnapped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Automatically snap objects with the "Grabbable" tag
        if (!isSnapped && other.CompareTag("Grabbable"))
        {
            StartCoroutine(SmoothSnap(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Unsnap when leaving the trigger area
        if (isSnapped && other.gameObject == currentSnappedObject)
        {
            Unsnap();
        }
    }
}
