using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// This script allows the player to shoot fireballs in a network game
public class ShootFireBall : NetworkBehaviour
{
    // Fireball prefab that will be spawned when shooting
    [SerializeField] private GameObject fireball;

    // Position and direction where the fireball will spawn
    [SerializeField] private Transform shootTransform;

    // List that stores all spawned fireballs
    [SerializeField] private List<GameObject> spawnedFireBalls = new List<GameObject>();    
    
    void Update()
    {
        // Only the owner of this object can shoot
        if (!IsOwner) return;

        // Shoot a fireball when the left mouse button is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Call a ServerRpc to spawn the fireball on the server
            ShootServerRpc();    
        }
    }

    // This function runs on the server
    [ServerRpc]
    private void ShootServerRpc()
    {
        // Create a fireball at the shoot position and rotation
        GameObject go = Instantiate(fireball, shootTransform.position, shootTransform.rotation);

        // Add the fireball to the list
        spawnedFireBalls.Add(go);

        // Set the parent script reference for the projectile
        go.GetComponent<MoveProjectile>().parent = this;

        // Spawn the object on the network so all clients can see it
        go.GetComponent<NetworkObject>().Spawn();
    }

    // ServerRpc that destroys a fireball
    // RequireOwnership = false means any client can request this
    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRpc()
    {
        // Get the first fireball in the list
        GameObject toDestroy = spawnedFireBalls[0];

        // Remove it from the network
        toDestroy.GetComponent<NetworkObject>().Despawn();

        // Remove it from the list
        spawnedFireBalls.Remove(toDestroy);

        // Destroy the GameObject
        Destroy(toDestroy);
    }
}