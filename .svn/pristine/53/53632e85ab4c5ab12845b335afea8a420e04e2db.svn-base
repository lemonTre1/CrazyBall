using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : NetworkLobbyManager {

    public static LobbyManager instance; 

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }  

    private void BeginGame()
    {
        Debug.Log("开始");
        ServerChangeScene(playScene);
    }
     
}
