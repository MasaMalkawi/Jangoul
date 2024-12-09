
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of movement
    public float height = 2f; // How high the object will move
    private float originalY; // Original Y position of the object
    private bool movingUp = true;

    void Start()
    {
        // Store the object's original Y position
        originalY = transform.position.y;
    }

    void Update()
    {
        // Calculate the new position
        float newY;

        if (movingUp)
        {
            newY = Mathf.Lerp(originalY, originalY + height, Mathf.PingPong(Time.time * moveSpeed, 1));
        }
        else
        {
            newY = Mathf.Lerp(originalY + height, originalY, Mathf.PingPong(Time.time * moveSpeed, 1));
        }

        // Set the new position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}