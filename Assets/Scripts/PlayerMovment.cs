using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// This script controls player movement in a networked game
public class PlayerMovment : NetworkBehaviour
{
    // Player movement speed
    [SerializeField] private float movementSpeed = 7f;

    // Player rotation speed
    [SerializeField] private float rotationSpeed = 500f;

    // Range used to give the player a random spawn position
    [SerializeField] private int randomPositionRange = 3;

    // Reference to the Animator component
    private Animator animator;

    
    void Start()
    {
        // Get the Animator component from this object
        animator = GetComponent<Animator>();
    }

    // This function runs when the object spawns on the network
    public override void OnNetworkSpawn()
    {
        // Call a ClientRpc to update the player position on all clients
        UpdatePositionClientRpc();
    }



    void Update()
    {
        // Only allow the owner of this object to control it
        if (!IsOwner) return;

        // Get player input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement direction using the input values
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        // Normalize keeps the movement speed consistent
        movementDirection.Normalize();

        // Move the player in the chosen direction
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        // Rotate the player to face the movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Update the animation depending on movement
        animator.SetFloat("run", movementDirection.magnitude);

    }

    // ClientRpc runs this function on all clients
    [ClientRpc]
    private void UpdatePositionClientRpc()
    {
        // Set a random starting position for the player
        transform.position = new Vector3(Random.Range(randomPositionRange, -randomPositionRange), 0, Random.Range(randomPositionRange, -randomPositionRange));

        // Set the starting rotation
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }
 
}