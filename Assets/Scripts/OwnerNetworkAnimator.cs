using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

// This class extends NetworkAnimator from Unity Netcode
public class OwnerNetworkAnimator : NetworkAnimator
{
    // This function tells Netcode who controls the animator state
    protected override bool OnIsServerAuthoritative()
    {
        // Return false means the Owner (the player who owns the object)
        // controls and sends the animation updates, not the server
        return false;
    }
}