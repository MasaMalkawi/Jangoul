
/*using System.Collections;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [Header("Snap Settings")]
    public Transform snapPosition; // Target position where the object snaps
    public float snapDistance = 0.5f; // Distance threshold to allow snapping
    public float snapDuration = 0.2f; // Duration for smooth snapping

    [Header("Firework Settings")]
    public GameObject fireworkPrefab; // Assign your firework prefab here
    public Transform fireworkSpawnPoint; // Optional: where the firework will spawn
    public int totalObjectsToSnap; // Total number of objects that must be snapped to trigger fireworks

    private bool isSnapped = false; // Whether an object is currently snapped
    private GameObject currentSnappedObject; // The currently snapped object
    public int snappedObjectCount = 0; // Tracks how many objects have been snapped

    private void Update()
    {
        // Check if an object is being held and close enough to snap
        if (currentSnappedObject == null || isSnapped) return;

        float distance = Vector3.Distance(currentSnappedObject.transform.position, snapPosition.position);
        if (distance <= snapDistance)
        {
            StartCoroutine(SmoothSnap(currentSnappedObject));
        }
    }

    // Smoothly snaps the object to the snap position.
    private IEnumerator SmoothSnap(GameObject obj)
    {
        isSnapped = true;
        currentSnappedObject = obj;

        // Snap object smoothly
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

        snappedObjectCount++;

        // Trigger fireworks when all objects are snapped
        if (snappedObjectCount >= totalObjectsToSnap)
        {
            TriggerFireworks();
        }
    }

    public void TriggerFireworks()
    {
        // Instantiate fireworks only when all objects are snapped
        if (fireworkPrefab != null)
        {
            // Instantiate at the spawn point or snap point
            Instantiate(fireworkPrefab, fireworkSpawnPoint ? fireworkSpawnPoint.position : transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Firework prefab is not assigned!");
        }
    }

    // Call this method when the object is grabbed.
    public void OnGrab(GameObject obj)
    {
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

    // Call this method when the object is released.
    public void OnRelease(GameObject obj)
    {
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

    // Unsnap the currently snapped object.
    public void Unsnap()
    {
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
   
}*/


using System.Collections;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [Header("Snap Settings")]
    public Transform snapPosition; // Target position where the object snaps
    public float snapDistance = 0.5f; // Distance threshold to allow snapping
    public float snapDuration = 0.2f; // Duration for smooth snapping
    public float offsetDistance = 0.1f; // Distance to keep the object in front of the canvas

    [Header("Firework Settings")]
    public GameObject fireworkPrefab; // Assign your firework prefab here
    public Transform fireworkSpawnPoint; // Optional: where the firework will spawn
    public int totalObjectsToSnap; // Total number of objects that must be snapped to trigger fireworks

    private bool isSnapped = false; // Whether an object is currently snapped
    private GameObject currentSnappedObject; // The currently snapped object
    public int snappedObjectCount = 0; // Tracks how many objects have been snapped

    private void Update()
    {
        // Check if an object is being held and close enough to snap
        if (currentSnappedObject == null || isSnapped) return;

        float distance = Vector3.Distance(currentSnappedObject.transform.position, snapPosition.position);
        if (distance <= snapDistance)
        {
            StartCoroutine(SmoothSnap(currentSnappedObject));
        }
    }

    // Smoothly snaps the object to the snap position with an offset
    private IEnumerator SmoothSnap(GameObject obj)
    {
        isSnapped = true;
        currentSnappedObject = obj;

        // Make the object temporarily kinematic to disable physics
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Vector3 startPosition = obj.transform.position;
        Quaternion startRotation = obj.transform.rotation;

        // Calculate the offset position in front of the snap position
        Vector3 offsetPosition = snapPosition.position + snapPosition.forward * offsetDistance;

        float elapsed = 0f;

        while (elapsed < snapDuration)
        {
            elapsed += Time.deltaTime;

            // Interpolate position and rotation smoothly
            obj.transform.position = Vector3.Lerp(startPosition, offsetPosition, elapsed / snapDuration);
            obj.transform.rotation = Quaternion.Slerp(startRotation, snapPosition.rotation, elapsed / snapDuration);

            yield return null;
        }

        // Finalize the position and rotation
        obj.transform.position = offsetPosition;
        obj.transform.rotation = snapPosition.rotation;

        snappedObjectCount++;

        // Trigger fireworks when all objects are snapped
        if (snappedObjectCount >= totalObjectsToSnap)
        {
            TriggerFireworks();
        }
    }

    // Trigger fireworks when all objects are snapped
    public void TriggerFireworks()
    {
        if (fireworkPrefab != null)
        {
            Instantiate(fireworkPrefab, fireworkSpawnPoint ? fireworkSpawnPoint.position : transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Firework prefab is not assigned!");
        }
    }

    // Call this method when the object is grabbed
    public void OnGrab(GameObject obj)
    {
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

    // Call this method when the object is released
    public void OnRelease(GameObject obj)
    {
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

    // Unsnap the currently snapped object
    public void Unsnap()
    {
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
}

