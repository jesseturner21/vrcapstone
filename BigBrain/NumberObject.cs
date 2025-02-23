/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberObject : MonoBehaviour
{
    [Header("Number Value")]
    public int numberValue;

    [Header("References")]
    public NumberSpawner spawner;

    private bool hasProcessedCollision = false;

    public bool isGrabbedLeft = false;
    public bool isGrabbedRight = true;

    public AudioSource collisionSound;

    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hasProcessedCollision)
            return;

        if (collisionSound != null && collision.transform.CompareTag("Object"))
        {
            collisionSound.Play();
        }

        NumberObject otherNumber = collision.gameObject.GetComponent<NumberObject>();
        if (otherNumber != null)
        {
            // Only process if one object is grabbed left and the other right.
            if ((isGrabbedLeft && otherNumber.isGrabbedRight) ||
                (isGrabbedRight && otherNumber.isGrabbedLeft))
            {
                hasProcessedCollision = true;
                otherNumber.hasProcessedCollision = true;

                int newValue = 0;
                // Determine ordering based on which object was grabbed from which side.
                if (isGrabbedLeft && otherNumber.isGrabbedRight)
                {
                    newValue = spawner.ExecuteOperation(numberValue, otherNumber.numberValue);
                }
                else if (isGrabbedRight && otherNumber.isGrabbedLeft)
                {
                    newValue = spawner.ExecuteOperation(otherNumber.numberValue, numberValue);
                }

                spawner.SpawnNumber(newValue, transform.position);

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

}
