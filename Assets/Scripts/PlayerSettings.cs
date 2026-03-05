using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

// This script sets player name and color for each network player
public class PlayerSettings : NetworkBehaviour
{
    // MeshRenderer used to change the player's color
    [SerializeField] private MeshRenderer meshRenderer;

    // UI text that shows the player's name
    [SerializeField] private TextMeshProUGUI playerName;

    // Network variable to store the player's name
    // Everyone can read it, but only the server can change it
    private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>(
        "Player: 0", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    // List of colors to give each player a different color
    public List<Color> colors = new List<Color>();

    private void Awake()
    {
        // Get the MeshRenderer from the child object
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // This function runs when the object spawns on the network
    public override void OnNetworkSpawn()
    {
        // Only the server sets the player name
        if (IsServer)  
        {
            // Create a player name using the client ID
            networkPlayerName.Value = "Player: " + (OwnerClientId + 1);
        }
        
        // Update the UI text with the player's name
        playerName.text = networkPlayerName.Value.ToString();

        // Set a color based on the player client ID
        meshRenderer.material.color = colors[(int)OwnerClientId];
    }
}