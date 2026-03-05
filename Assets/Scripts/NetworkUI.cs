using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    // Button to start the game as Host
    [SerializeField] private Button hostButton;

    // Button to join the game as Client
    [SerializeField] private Button clientButton;

    // UI text to show how many players are connected
    [SerializeField] private TextMeshProUGUI playersCountText;

    // Network variable that stores the number of players
    // Everyone can read it, but only the server should change it
    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    private void Awake()
    {
        // When host button is clicked, start the game as Host
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        // When client button is clicked, connect as Client
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }

    private void Update()
    {
        // Update the UI text to show the current number of players
        playersCountText.text = "Players: " + playersNum.Value.ToString();

        // If this object is not running on the server, stop here
        if (!IsServer) return;

        // Server updates the player count based on connected clients
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}