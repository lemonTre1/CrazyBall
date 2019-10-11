﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerController :  NetworkBehaviour{
    
    public PlayerAttributes m_PlayerAttributes;
    Rigidbody m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        CmdCreatMap(LobbyUI._Instance.mapInfo,LobbyUI._Instance.m_vecotr2);

        int playerID = playerControllerId + 1;
        Camera.main.GetComponent<Camerafollow>().target = transform.gameObject;

        m_PlayerAttributes = new PlayerAttributes("player0" + playerID, 1, 1, 1);
        m_PlayerAttributes.MaxSpeed = 20;
        m_PlayerAttributes.ExpansionMultiple = 1;
        transform.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("ball/0" + playerID);
        gameObject.AddComponent<SkillController>();

        Reset();
    }


    public void Reset()
    {

        transform.position =new Vector3( Vector3.zero.x+Random.Range(-50,50), Vector3.zero.y+5,Vector3.zero.z + Random.Range(-50, 50));
        if (m_rigidbody==null)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
        m_rigidbody.velocity = Vector3.zero;
    }


    void CmdCreatMap(MapType mt,List<Vector2> listValue)
    {
        GameObject floorParent = MapManager.instance.CmdCreatMap(mt, listValue);
        //NetworkServer.Spawn(floorParent.gameObject);
        //for (int i = 0; i < listValue.Count; i++)
        //{
        //    Debug.Log(listValue[i].ToString());
        //}
    }
     
}
