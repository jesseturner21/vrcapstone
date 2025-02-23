
/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using UnityEngine;

public class SpinAround : MonoBehaviour
{
    // Rotation speed in degrees per second.
    public float spinSpeed = 30f;

    void Update()
    {
        // Rotate around the Y-axis.
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
