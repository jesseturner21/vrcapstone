/*
 * Author: Jesse Turner
 * Date: 1/27/2025
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToObjectTransform : MonoBehaviour
{
    // Reference to the VR player rig (set this in the Inspector)
    public GameObject playerRig;

    // how small the player should become
    public float scaleFactor;

    // Call this function to teleport the player
    public void TeleportPlayerToObject()
    {
        if (playerRig != null)
        {
            // Shrink the User
            playerRig.transform.localScale = playerRig.transform.localScale * scaleFactor;

            // Get the object's transform position
            Vector3 targetPosition = transform.position;

            // Move the player rig to the object's position
            playerRig.transform.position = targetPosition;

            // Reset the rotation to original
            playerRig.transform.rotation = Quaternion.identity;



            Debug.Log("Player teleported to the object's position!");
        }
        else
        {
            Debug.LogError("Player rig is not assigned!");
        }
    }
}

