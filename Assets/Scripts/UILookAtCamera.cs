using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script makes a UI object always face the camera
public class UILookAtCamera : MonoBehaviour
{

    void LateUpdate()
    {
        // Rotate the object so it always looks toward the camera direction
        // This is useful for world-space UI like player name labels
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}