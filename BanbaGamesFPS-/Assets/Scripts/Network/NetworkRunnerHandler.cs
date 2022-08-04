using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;
    
    NetworkRunner networkRunner;
    void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "Network runner";

        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);

        Debug.Log($"Server NetworkRunner started");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, Action<NetworkRunner> initalized) {
        {
            var sceneObjectProvider = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

            if(sceneObjectProvider == null){
                // handle networked objects that already exits in the scene
                sceneObjectProvider = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();

            }

            runner.ProvideInput = true;

            return runner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                Address = address,
                Scene = scene,
                SessionName = "TestBoom",
                Initialized = initalized,
                SceneManager = sceneObjectProvider
            });
        }
    }

}
