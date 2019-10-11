﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RankPanel : MonoBehaviour {

    public GameObject res_RankItem;
    public Transform rankItemParent;

    public List<RankItem> rankItemList = new List<RankItem>();

    private static RankPanel instance;
    public static RankPanel _Instance
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

        //if (isServer)
        //{
        //    Invoke("CmdCreateRankItem", 1.0f);
        //}

	}
	
	void Update () {
		

	}

    //[Command]
    void CmdCreateRankItem() {

        for (int i = 0; i < GameManager.Instance.playerDatas.Count; i++)
        {
            GameObject rankItem_go = Instantiate(res_RankItem, rankItemParent);
            RankItem rankItem = rankItem_go.GetComponent<RankItem>();
            if (rankItem == null)
            {
                rankItem= rankItem_go.AddComponent<RankItem>();
            } 
            rankItem.target = GameManager.Instance.playerDatas[i].gameObject;
            rankItem.name_str = GameManager.Instance.playerDatas[i].PlayerName;
            rankItemList.Add(rankItem);
            //NetworkServer.Spawn(rankItem_go);
        }

    }




}
