using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of movement
    public float width = 2f; // How far the object will move left and right
    private float originalX; // Original X position of the object
    private bool movingRight = true;

    void Start()
    {
        // Store the object's original X position
        originalX = transform.position.x;
    }

    void Update()
    {
        // Calculate the new position
        float newX;

        if (movingRight)
        {
            newX = Mathf.Lerp(originalX, originalX + width, Mathf.PingPong(Time.time * moveSpeed, 1));
        }
        else
        {
            newX = Mathf.Lerp(originalX + width, originalX, Mathf.PingPong(Time.time * moveSpeed, 1));
        }

        // Set the new position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
