using System.Collections;
using UnityEngine;

public class FireworkManager : MonoBehaviour
{
    public GameObject fireworkPrefab; // Firework prefab to instantiate
    public Transform fireworkSpawnPoint; // Spawn location for the fireworks
    public GameObject[] objectsToSnap; // Objects that need to be snapped
    private bool[] snappedStatus; // Track snapped status for each object
    private bool fireworkTriggered = false;

    void Start()
    {
        // Initialize snapped status array
        snappedStatus = new bool[objectsToSnap.Length];

        // Ensure firework is not active at the start
        if (fireworkPrefab != null)
        {
            fireworkPrefab.SetActive(false);
        }
    }

    public void ObjectSnapped(int index)
    {
        if (index >= 0 && index < snappedStatus.Length)
        {
            snappedStatus[index] = true;

            // Check if all objects are snapped
            if (AllObjectsSnapped() && !fireworkTriggered)
            {
                TriggerFirework();
            }
        }
    }

    private bool AllObjectsSnapped()
    {
        foreach (bool snapped in snappedStatus)
        {
            if (!snapped) return false;
        }
        return true;
    }

    private void TriggerFirework()
    {
        fireworkTriggered = true;

        if (fireworkPrefab != null)
        {
            // Activate firework or instantiate it at a specific location
            GameObject firework = Instantiate(fireworkPrefab, fireworkSpawnPoint.position, Quaternion.identity);
            firework.SetActive(true);

            // Optional: Destroy the firework after a delay
            Destroy(firework, 5f);
        }
    }

    public void TriggerFireworkOnNextButton()
    {
        Debug.Log("Next button clicked. Triggering firework.");
        if (fireworkPrefab != null && fireworkSpawnPoint != null)
        {
            GameObject firework = Instantiate(fireworkPrefab, fireworkSpawnPoint.position, Quaternion.identity);
            firework.SetActive(true);

            // Optional: Destroy the firework after 5 seconds
            Destroy(firework, 5f);
        }
        else
        {
            Debug.LogWarning("Firework prefab or spawn point is not assigned.");
        }
    }
}