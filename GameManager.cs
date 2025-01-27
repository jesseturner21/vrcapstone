/*
 * Author: Jesse Turner
 * Date: 1/27/2025
 */
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using System.Collections.Generic;


public class GameManager : NetworkBehaviour
{
    // location of Seeker Spawn
    public Transform seekerLocation;
    // location of Hider Spawn
    public Transform hiderLocation;

    private GameObject thisPlayer;

    // Keep track of which clients are assigned to which role.
    // Key = ClientId, Value = "Seeker" or "Hider"
    private Dictionary<ulong, string> playerRoles = new Dictionary<ulong, string>();
    void Start()
    {
        if(thisPlayer == null)
        {
            thisPlayer = GameObject.FindWithTag("Player");
        }
    }

    // This method can be hooked up to a UI button that only the Host can click.
    public void OnStartGameButtonClicked()
    {
        Debug.LogError("Start Clicked");
        // We only want the Host (Server) to call the StartGame. 
        if (!IsServer) return;

        StartGame();
    }

    // Called on the server to start the game and assign roles
    private void StartGame()
    {
        // Example: We only handle 2 players for simplicity.
        // In a real scenario, you'd want more robust logic (e.g. if we have exactly 2 players).
        if (NetworkManager.Singleton.ConnectedClients.Count < 2)
        {
            Debug.LogError("Not enough players to start the game.");
            return;
        }

        // Gather the connected clients (Host + client(s))
        List<ulong> clientIds = new List<ulong>(NetworkManager.Singleton.ConnectedClients.Keys);

        // For demonstration, we assume the first in the list is the Seeker, second is the Hider
        // Or you can do random assignment.
        ulong seekerId = clientIds[0];
        ulong hiderId = clientIds[1];

        playerRoles[seekerId] = "Seeker";
        playerRoles[hiderId] = "Hider";
        Debug.LogError("Roles Assigned");

        // Notify the clients about their roles via a ClientRpc
        NotifyRolesClientRpc(seekerId, hiderId);
        //SetUpSeeker(seekerId);
        //SetUpHider(hiderId);
    }

    [ClientRpc]
    private void NotifyRolesClientRpc(ulong seekerId, ulong hiderId)
    {
        ulong localClientId = NetworkManager.Singleton.LocalClientId;
        // SEEKER
        if (localClientId == seekerId)
        {
            thisPlayer.transform.position = seekerLocation.position;
            thisPlayer.transform.rotation = seekerLocation.rotation;
        }
        // HIDER
        else if (localClientId == hiderId)
        {
            thisPlayer.transform.position = hiderLocation.position;
            thisPlayer.transform.rotation = hiderLocation.rotation;
        }
    }

    private void SetUpSeeker(ulong seekerId)
    {
        GameObject playerObject = GetPlayerObjectByClientId(seekerId);

        if (playerObject != null)
        {
            // Give the Seeker the gun
            Transform rightHand = playerObject.transform.Find("Right Hand");
            if (rightHand != null)
            {
                Transform revolver = rightHand.Find("revolver");
                if (revolver != null)
                {
                    revolver.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError($"Revolver not found on {playerObject.name}");
                }
            }
            else
            {
                Debug.LogError($"Right Hand not found on {playerObject.name}");
            }

            // Teleport Seeker to location
            if (seekerLocation != null)
            {
                TeleportPlayerServerRpc(seekerId, seekerLocation.position, seekerLocation.rotation);
            }
            else
            {
                Debug.LogError("Seeker location is not assigned!");
            }
        }
        else
        {
            Debug.LogError($"Player object for seekerId {seekerId} not found.");
        }
    }

    private void SetUpHider(ulong hiderId)
    {
        TeleportPlayerServerRpc(hiderId, hiderLocation.position, hiderLocation.rotation);
    }
    private GameObject GetPlayerObjectByClientId(ulong clientId)
    {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out var clientData))
        {
            if (clientData.PlayerObject != null)
            {
                return clientData.PlayerObject.gameObject;
            }
            else
            {
                Debug.LogError($"PlayerObject for ClientId {clientId} is null.");
            }
        }
        else
        {
            Debug.LogError($"Client with ClientId {clientId} not found in ConnectedClients.");
        }

        return null;
    }

    [ClientRpc]
    private void UpdatePlayerPositionClientRpc(ulong clientId, Vector3 position, Quaternion rotation)
    {
        GameObject playerObject = GetPlayerObjectByClientId(clientId);
        if (playerObject != null)
        {
            playerObject.transform.position = position;
            playerObject.transform.rotation = rotation;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void TeleportPlayerServerRpc(ulong clientId, Vector3 position, Quaternion rotation)
    {
        if (IsServer)
        {
            // Update on server
            GameObject playerObject = GetPlayerObjectByClientId(clientId);
            if (playerObject != null)
            {
                Debug.LogError($"Player postion: {playerObject.transform.position}/ To position: {position}");
                playerObject.transform.position = position;
                playerObject.transform.rotation = rotation;
                Debug.LogError($"Player postion: {playerObject.transform.position}/ To position: {position}");

                // Notify clients
                UpdatePlayerPositionClientRpc(clientId, position, rotation);
            }
        }
    }

}

