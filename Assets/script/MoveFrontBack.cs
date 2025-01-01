using UnityEngine;

public class MoveFrontBack : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of movement
    public float depth = 2f; // How far the object will move front and back
    private float originalZ; // Original Z position of the object
    private bool movingForward = true;

    void Start()
    {
        // Store the object's original Z position
        originalZ = transform.position.z;
    }

    void Update()
    {
        // Calculate the new position
        float newZ;

        if (movingForward)
        {
            newZ = Mathf.Lerp(originalZ, originalZ + depth, Mathf.PingPong(Time.time * moveSpeed, 1));
        }
        else
        {
            newZ = Mathf.Lerp(originalZ + depth, originalZ, Mathf.PingPong(Time.time * moveSpeed, 1));
        }

        // Set the new position
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}

