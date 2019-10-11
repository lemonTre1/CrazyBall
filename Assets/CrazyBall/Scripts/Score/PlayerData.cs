﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{

    [SyncVar(hook = "OnScroeChanged")]
    public int Scroe = 0;
    /// <summary>
    /// 玩家编号
    /// </summary>
    [SyncVar(hook = "OnPlayerNumChanged")]
    public int PlayerNum = -1;
    /// <summary>
    /// 攻击者编号
    /// </summary>
    [SyncVar(hook = "OnAttackerNumChanged")]
    public int Attacker = -1;

    [SyncVar(hook = ("OnChangeName"))]
    public string strPlayerName;

    public string PlayerName
    {
        get
        {
            return strPlayerName;
        }
        set
        {
            strPlayerName = value;
        }
    }

    private RankItem rankItem;

    void Start()
    {
        StartCoroutine(Init());
        //Debug.Log("PlayerData::Start()..");
    }
    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("2222");


        GameManager.Instance.playerDatas.Add(this);

        GameObject rankItem_go = Instantiate(RankPanel._Instance.res_RankItem, RankPanel._Instance.rankItemParent);
        rankItem = rankItem_go.GetComponent<RankItem>();
        if (rankItem == null)
        {
            rankItem = rankItem_go.AddComponent<RankItem>();
        }
        rankItem.target = this.gameObject;
        rankItem.name_str = this.PlayerName;
        //Debug.Log("this.PlayerName:" + this.PlayerName);
        RankPanel._Instance.rankItemList.Add(rankItem);

        int index = GameManager.Instance.GetIndex(this);
        if (index != -1)
        {
            Init(index);
        }
        yield return 0;
    }
    public override void OnStartLocalPlayer()
    {
        //Debug.LogError("OnStartLocalPlayer =" + Time.time);

        base.OnStartLocalPlayer();
        CmdOnChangeName(LobbyUI._Instance.m_name);
        //SetupLocalPlayer();
        //Debug.Log("PlayerData::OnStartLocalPlayer()" + PlayerName);
    }
    
    [Command]
    void CmdOnChangeName(string name) {

        PlayerName = name;

    }

    void OnChangeName(string nameValue) {
        PlayerName = nameValue;

        if (rankItem!=null)
           rankItem.name_str = this.PlayerName;
    }

    public void UpdateIndex()
    {
        int index = GameManager.Instance.GetIndex(this);
        if (index != -1)
        {
            Init(index);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.playerDatas.Remove(this);
    }
    void Update()
    {

    }
    public void Init(int _index)
    {
        PlayerNum = _index;
        Clear();
    }

    [Command]
    public void CmdScore()
    {
        Scroe++;

       

    }
    public void Clear()
    {
        Attacker = -1;
    
    }

    [Server]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Attacker = collision.transform.GetComponent<PlayerData>().PlayerNum;
        }
    }

    private void OnScroeChanged(int newValue)
    {
        Scroe = newValue;

        for (int i = 0; i < RankPanel._Instance.rankItemList.Count; i++)
        {
            if (this.gameObject == RankPanel._Instance.rankItemList[i].target)
            {
                RankItem rankItem = RankPanel._Instance.rankItemList[i];
                rankItem.score_int = Scroe;
            }
        }


        Debug.Log("玩家" + PlayerNum + "Score=" + Scroe);
    }

    private void OnPlayerNumChanged(int newValue)
    {
        PlayerNum = newValue;
    }

    private void OnAttackerNumChanged(int newValue)
    {
        Attacker = newValue;
        if(newValue==-1)
          transform.GetComponent<PlayerController>().Reset();
    }
}
