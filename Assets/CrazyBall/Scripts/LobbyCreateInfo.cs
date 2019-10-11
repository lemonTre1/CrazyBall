using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCreateInfo : MonoBehaviour { 

    public string roomInfo = "";

    public MapType m_mapType = MapType.Grass;

    void Start () {
		

	}
	
	void Update () {
		

	}


    public void CreatMapInfo()
    {
        LobbyUI._Instance.CreateHost(m_mapType);
        //GetComponentInParent<LobbyUI>().CreateHost(m_mapType);
        //LobbyUI._Instance.CreateHost();
        
    }

}
