/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOperation : MonoBehaviour
{
    public NumberObject.Operation ballOperation;

    // The Number Spawner that will perform the Operations
    public NumberSpawner numSpawner;

    //Material To change the hands to
    public Material materialForHands;

    // Synthetic hands to change 
    public SkinnedMeshRenderer leftHandRenderer;
    public SkinnedMeshRenderer rightHandRenderer;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with is tagged as "Ball"
        if (other.gameObject.CompareTag("Player"))
        {
            if(numSpawner != null)
            {
                numSpawner.selectedOperation = ballOperation;
            }
            if(leftHandRenderer != null && rightHandRenderer != null)
            {
                leftHandRenderer.material = materialForHands;
                rightHandRenderer.material = materialForHands;
            }
        }
    }
}
