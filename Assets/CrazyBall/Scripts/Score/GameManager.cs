﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class GameManager : NetworkBehaviour
{
    public static GameManager Instance = null;
    public List<PlayerData> playerDatas = new List<PlayerData>();
    [SerializeField]
    private float Index;
    [SerializeField]
    [SyncVar(hook = "OnGameTimeChanged")]
    private float GameTime=300;
    [SerializeField]
    private bool GameStart = true;
    [SerializeField]
    private Text teGameTime;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        Debug.Log("111111");
    }
    void Start()
    {

    }
    void Init()
    {
        playerDatas.Clear();
        GameStart = true;
    }
    public int GetIndex(PlayerData data)
    { 
        if (playerDatas.Contains(data))
        {
            for (int i = 0; i < playerDatas.Count; i++)
            {
                if (playerDatas[i] == data)
                {
                    return i;
                }

            }
        }
        else
        {
            return -1;
        }

        return -1;

    }
    // Update is called once per frame
    void Update()
    {
        int _time = Mathf.RoundToInt(GameTime);
        teGameTime.text = GetTime(_time);
        //Debug.Log(GetTime(_time));
        if (GameStart)
        {
            if (isServer && GameTime > 0)
            {
                GameTime -= Time.deltaTime;
                foreach (var _player in playerDatas)
                {
                    if (_player.transform.position.y < Index && _player.Attacker != -1)
                    {
                        playerDatas[_player.Attacker].CmdScore();
                        _player.Clear();
                    }
                    else if (_player.Attacker == -1 && _player.transform.position.y < Index)
                    {
                        _player.Attacker = -2;
                        _player.Clear();
                    }
                }
            }
            else if(GameTime <= 0)
            {
                GameStart = false;
            } 
        }
        else
        {
            GameOver();
        }
      
       
    }
    public void OnGameTimeChanged(float newValue)
    {
        GameTime = newValue;
    }
    private void GameOver()
    {
        GameStart = false;
        Debug.Log("游戏结束");
    }
    string GetTime(float time)
    {
        float h = Mathf.FloorToInt(time / 3600f);
        float m = Mathf.FloorToInt(time / 60f - h * 60f);
        float s = Mathf.FloorToInt(time - m * 60f - h * 3600f);
        return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
    }

}
