using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    
    public NetworkPlayer playerPrefab;
    void Start()
    {
        
    }

    public void OnConnectedToServer(NetworkRunner runner) {

        Debug.Log("OnConnectedToServer");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player){

        if(runner.IsServer){
            Debug.Log("OnPlayerJoined we are server. Spawning player");
            runner.Spawn(playerPrefab, Utils.GetRandomSpawnPoint(), Quaternion.identity, player);

        }
        else Debug.Log("OnPlayerJoined");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input){

        
    }

    public void OnPlayerLeft(NetworkRunner runner,PlayerRef player){}
    public void OnInputMissing(NetworkRunner runner,PlayerRef player, NetworkInput ınput){}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason){Debug.Log("OnShutdown");}
    public void OnDisconnectedFromServer(NetworkRunner runner){Debug.Log("OnDisconnectedFromServer");}
    public void OnConnectRequest(NetworkRunner runnner,NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token){Debug.Log("OnConnectRequest"); }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAdress, NetConnectFailedReason reason){ Debug.Log("OnConnectFailed");}

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message){}

    public void OnSessionListUpdated(NetworkRunner runner,List<SessionInfo> sessionList){}

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data){}
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken){}

    public void OnReliableDataReceived(NetworkRunner runner,PlayerRef player, ArraySegment<byte> data){}

    public void OnSceneLoadDone(NetworkRunner runner){}
    public void OnSceneLoadStart(NetworkRunner runner){}


}
