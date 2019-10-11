using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerIP : MonoBehaviour {

    public int id;

    public string ip;

    public Text name;

    public MapType mapType;

    public List<Vector2> m_vector2 = new List<Vector2>();
    public void JoinClient() { 

        LobbyUI._Instance.joinIP=ip;
        LobbyUI._Instance.mapInfo = mapType;
        LobbyUI._Instance.m_vecotr2.Clear();
        for (int i = 0; i < m_vector2.Count; i++)
        {
            LobbyUI._Instance.m_vecotr2.Add(m_vector2[i]);
        }
    }

}
