using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectGroups; // Parent GameObjects for each canvas

    void Start()
    {
        // Ensure only the first group is active at the start
        ShowObjects(0);
    }

    // Method to show the specified object group (parent) and hide others
    public void ShowObjects(int index)
    {
        for (int i = 0; i < objectGroups.Length; i++)
        {
            objectGroups[i].SetActive(i == index); // Show the correct group, hide others
        }
    }
}