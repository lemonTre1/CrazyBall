using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class GameClient : MonoBehaviour {

    public string Client_IP;

    private static GameClient instance;
    public static GameClient _Instance
    {
        get {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start () {

        getClientIP();

    }



    void Update () {
		
	}


    void getClientIP()
    {
        IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

        foreach (IPAddress ip in ips)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Client_IP = ip.ToString();
            }
        }
    }

}
