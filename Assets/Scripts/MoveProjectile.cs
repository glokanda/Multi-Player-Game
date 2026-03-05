using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// This script controls how the projectile (fireball) moves and what happens when it hits something
public class MoveProjectile : NetworkBehaviour
{
    // Reference to the script that created this fireball
    public ShootFireBall parent;

    // Particle effect that appears when the fireball hits something
    [SerializeField] private GameObject hitParticles;

    // How strong the fireball moves forward
    [SerializeField] private float shootForce;

    // Rigidbody used to move the fireball with physics
    private Rigidbody rb;


    void Start()
    {
        // Get the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move the fireball forward based on the direction it is facing
        rb.linearVelocity = rb.transform.forward * shootForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only the owner of this object should run this code
        if (!IsOwner) return;

        // Tell the server to create the hit particle effect
        InstantiateHitParticlesServerRpc();

        // Tell the parent script to destroy the fireball on the server
        parent.DestroyServerRpc();
    }

    [ServerRpc]
    private void InstantiateHitParticlesServerRpc()
    {
        // Create the hit particle effect at the fireball position
        GameObject hitImpact = Instantiate(hitParticles, transform.position, Quaternion.identity);

        // Spawn the particle object on the network so all players can see it
        hitImpact.GetComponent<NetworkObject>().Spawn();

        // Rotate the particle effect to the correct angle
        hitImpact.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
    }
}