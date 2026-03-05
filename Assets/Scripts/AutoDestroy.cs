using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// This script automatically destroys a network object after some time
public class AutoDestroy : NetworkBehaviour
{
    // Time to wait before destroying the object
    public float delayBeforeDestroy = 5f;

    // ServerRpc means this function runs on the server
    // RequireOwnership = false allows any client to call this RPC
    [ServerRpc(RequireOwnership = false)]
    private void DestroyParticlesServerRpc()
    {
        // Remove the object from the network so it no longer exists for clients
        GetComponent<NetworkObject>().Despawn();

        // Destroy the GameObject after a delay
        Destroy(gameObject, delayBeforeDestroy);
    }

}