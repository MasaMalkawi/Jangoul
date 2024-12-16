using System.Collections;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public Transform snapPosition; // The position and rotation where objects will snap

    private bool IsSnapped = false; // Tracks if an object is snapped
    private GameObject currentSnappedObject; // Tracks the currently snapped object

    public void UnsnapFromBoard()
    {
        if (currentSnappedObject != null)
        {
            Rigidbody rb = currentSnappedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            currentSnappedObject = null;
        }
        IsSnapped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentSnappedObject != null || IsSnapped) return; // Prevent multiple objects

        if (other.CompareTag("Grabbable"))
        {
            StartCoroutine(SmoothSnap(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable") && other.gameObject == currentSnappedObject)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            currentSnappedObject = null;
            IsSnapped = false;
        }
    }

    private IEnumerator SmoothSnap(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        float duration = 0.2f; // Smooth snap duration
        Vector3 startPosition = obj.transform.position;
        Quaternion startRotation = obj.transform.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPosition, snapPosition.position, elapsed / duration);
            obj.transform.rotation = Quaternion.Slerp(startRotation, snapPosition.rotation, elapsed / duration);
            yield return null;
        }

        obj.transform.position = snapPosition.position;
        obj.transform.rotation = snapPosition.rotation;

        currentSnappedObject = obj;
        IsSnapped = true;
    }
}