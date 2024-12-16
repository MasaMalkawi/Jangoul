using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject fireworksPrefab; // Fireworks prefab
    [SerializeField] private float fireworksDuration = 30f; // Duration for fireworks to stay

    [Header("Snappable Objects")]
    public GameObject[] objectsToPlace; // Objects that need to be snapped to the canvas

    private bool fireworksTriggered = false; // To ensure fireworks only trigger once

    void Update()
    {
        // Check if all objects are snapped
        if (!fireworksTriggered && AreAllObjectsSnapped())
        {
            TriggerFireworks();
            fireworksTriggered = true;
        }
    }

    // Check if all objects are snapped to the canvas
    private bool AreAllObjectsSnapped()
    {
        foreach (GameObject obj in objectsToPlace)
        {
            MagneticObject magnet = obj.GetComponent<MagneticObject>();
            if (magnet == null || !magnet.IsSnapped)
            {
                return false;
            }
        }
        return true;
    }

    // Trigger fireworks effect when all objects are snapped
    private void TriggerFireworks()
    {
        Debug.Log("All objects are snapped! Triggering fireworks.");

        // Instantiate fireworks at the position of the first object or any desired position
        Vector3 fireworksPosition = objectsToPlace[0].transform.position; // You can change this if you want a different position
        Instantiate(fireworksPrefab, fireworksPosition, Quaternion.identity);

        StartCoroutine(DestroyFireworksAfterTime(fireworksDuration));
    }

    // Coroutine to destroy fireworks after the specified duration
    private IEnumerator DestroyFireworksAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Find all fireworks objects and destroy them
        GameObject[] activeFireworks = GameObject.FindGameObjectsWithTag("Fireworks");
        foreach (GameObject firework in activeFireworks)
        {
            Destroy(firework);
        }

        Debug.Log("Fireworks destroyed.");
    }
}