/*
*  Author: Jesse Turner
*  Date: 2-22-2025
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;

public class HandGrabInteractorWithAction : HandGrabInteractor
{
    public bool isLeftHand;
    // When a Number is grabbed mark it as isGrabbed
    private NumberObject previousNumRight;
    private NumberObject previousNumLeft;
    protected override void InteractableSelected(HandGrabInteractable interactable)
    {
        base.InteractableSelected(interactable);
        NumberObject numObj = interactable.GetComponentInParent<NumberObject>();
        if(numObj != null)
        {
            // if this is the left hand mark on object, otherwise it is the right hand
            if (isLeftHand)
            {
                // if there is a previous number turn off the selection
                if (previousNumLeft != null) previousNumLeft.isGrabbedLeft = false;
                numObj.isGrabbedLeft = true;
                previousNumLeft = numObj;
                
            }
            else
            {
                if (previousNumRight != null) previousNumRight.isGrabbedRight = false;
                numObj.isGrabbedRight = true;
                previousNumRight = numObj;
            }
        }
        else
        {
            Debug.LogError("Missing the number Object");
        }
    }

    
}
