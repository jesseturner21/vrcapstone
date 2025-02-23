/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using UnityEngine;

public class BounceUpAndDown : MonoBehaviour
{
    // How high the object bounces.
    public float amplitude = 0.05f;
    // How fast the object bounces.
    public float frequency = 0.5f;
    // Offset value to stagger the bouncing start time.
    public float timeOffset = 0f;

    private Vector3 startPos;

    void Start()
    {
        // Store the starting position.
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate new Y position using a sine wave with the time offset.
        float newY = startPos.y + amplitude * Mathf.Sin((Time.time + timeOffset) * frequency * 2 * Mathf.PI);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
