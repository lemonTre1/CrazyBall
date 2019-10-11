﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyUI : NetworkBehaviour {


    public LobbyManager m_manager;
    public LobbyDiscovery m_discovery;
    public NetworkClient m_client;

    public Transform playerParent;

    //服务器列表父级
    public Transform m_serverParent;
    //服务器列表
    public List<GameObject> m_servers = new List<GameObject>();
    //服务器预制体
    public GameObject prefabServer; 

    public string statusInfo;
    public string ipInfo;

    [Header("房间列表")]
    public Button refreshRoomInfo;
    public Button closeRoomInfo;
    public Button createHost;
    public Button joinButton;
    public InputField nameInput;
    public GameObject roomInfoGo;

    [Header("创建房间")]
    public GameObject CreatRoomGo;
    public Button colseCreatRoomGo;
    public MapType mapInfo;

    [Header("地图信息")]
    public int length=10;
    public int width=10;
    public int FloorNum=10;
    public List<Vector2> m_vecotr2 = new List<Vector2>();


    [Header("房间信息")]
    public GameObject RoomGo;
    public Button closeRoom;
    public Button buttonready;
    public Button buttonstart;


    [Header("提示信息")]
    public GameObject tipsInfo;
    public Button tipButton;
    public Text tipText;



    [SyncVar]
    public string m_name;
    public string joinIP;


    private static LobbyUI instance;
    public static LobbyUI _Instance
    {
        get {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        refreshRoomInfo.onClick.AddListener(RecUDP);
        closeRoomInfo.onClick.AddListener(ButtonCloseRoomInfo);
        createHost.onClick.AddListener(ButtoncreateHost);
        joinButton.onClick.AddListener(ButtonJoin);
        nameInput.onEndEdit.AddListener(InputName);

        colseCreatRoomGo.onClick.AddListener(ButtoncolseCreatRoomGo);


        closeRoom.onClick.AddListener(ButtoncolseRoomGo);


        tipButton.onClick.AddListener(TipsButton);
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        //ButtonRefreshRoomInfo();
    }

    /// <summary>
    /// 刷新房间信息
    /// </summary>
    void ButtonRefreshRoomInfo() { 

        RecUDP();

    }

    /// <summary>
    /// 关闭房间信息
    /// </summary>
    void ButtonCloseRoomInfo() {

        //roomInfoGo.SetActive(false);

    }

    /// <summary>
    /// 创建服务器
    /// </summary>
    void ButtoncreateHost() {

        if (string.IsNullOrEmpty(m_name))
        {
            ShowTips("请输入姓名后再创建房间");
            return;
        }

        CreatRoomGo.SetActive(true);
        roomInfoGo.SetActive(false);

    }

    /// <summary>
    /// 加入服务器
    /// </summary>
    void ButtonJoin() { 

        if (string.IsNullOrEmpty(m_name))
        {
            ShowTips("请输入姓名后再创建房间");
            return;
        }

        if (string.IsNullOrEmpty(joinIP))
        {
            ShowTips("请选择一个房间");
            return;
        }

        JoinClient(joinIP); 

        roomInfoGo.gameObject.SetActive(false);

        RoomGo.gameObject.SetActive(true);
    }

    /// <summary>
    /// 输入的名称
    /// </summary>
    /// <param name="value"></param>
    void InputName(string value) {

        m_name = value;

    }

    /// <summary>
    /// 关闭创建房间面板
    /// </summary>
    void ButtoncolseCreatRoomGo() {

        CreatRoomGo.SetActive(false);
        roomInfoGo.SetActive(true);
        joinIP = "";

    }

    void ButtoncolseRoomGo() { 

        StopUDP();
        Debug.Log("ButtoncolseRoomGo");
        GetComponent<LobbyManager>().StopHost();
        GetComponent<LobbyManager>().StopClient();
        //RoomGo.SetActive(false);
        //roomInfoGo.SetActive(true);
        //ActiveRoomButton(true);

        Destroy(transform.parent.gameObject);

    }



    //发送广播
    void SendUDP()
    {
        //先停止接收
        StopUDP();

        m_discovery.Initialize();

        //发送
        m_discovery.StartAsServer(); 
        //print("开始发送广播！");
    }

    //接收广播
    public void RecUDP()
    {
        if (!m_discovery.isClient)
        {
            for (int i = 0; i < m_serverParent.childCount; i++)
            {
                Destroy(m_serverParent.GetChild(i).gameObject);
            }
            m_servers.Clear();

            bool isTrue = m_discovery.Initialize();
             
            m_discovery.StartAsClient();

            Invoke("IsServer", 1f);

            print("开始扫描服务器！"); 
           
        }
    }

    //停止广播
    void StopUDP()
    {
        if (m_discovery.isClient || m_discovery.isServer)
        {
            m_discovery.StopBroadcast();
        }
        
    }

    //找到服务器
    void IsServer()
    {
        Debug.Log("IsServer............");
        if (m_discovery.running)
        {
            //服务器列表是否大于0
            if (m_discovery.broadcastsReceived.Count > 0)
            {
                List<string> ip = new List<string>();
                List<byte[]> data = new List<byte[]>();
                //遍历服务器列表
                foreach (var key in m_discovery.broadcastsReceived.Keys)
                {
                    NetworkBroadcastResult value;
                    if (m_discovery.broadcastsReceived.TryGetValue(key, out value))
                    {
                        //添加服务器预设到列表
                        m_servers.Add(Instantiate(prefabServer, m_serverParent));
                        ip.Add(value.serverAddress);
                        data.Add(value.broadcastData);
                    }
                }
                for (int i = 0; i < m_servers.Count; i++)
                {
                    m_servers[i].GetComponent<ServerIP>().id = i;
                    m_servers[i].GetComponent<ServerIP>().ip = ip[i];
                    m_servers[i].GetComponent<ServerIP>().name.text = ip[i];
                    string arr_str = System.Text.Encoding.Unicode.GetString(data[i]);
                    Debug.Log("mapStr:" + arr_str);


                    string[] arr_data = arr_str.Split('_');

                    m_servers[i].GetComponent<ServerIP>().mapType = (MapType)System.Enum.Parse(typeof(MapType), arr_data[0]);
                    
                    for (int j = 1; j < arr_data.Length; j++)
                    {
                        string strTemp = arr_data[j].Trim(new char[] { '(', ')' });
                        string[] vec_data = strTemp.Split(',');
                        float vecX = float.Parse(vec_data[0]);
                        float vecY = float.Parse(vec_data[1]); 
                        Vector2 vecTemp = new Vector2(vecX, vecY);
                        m_servers[i].GetComponent<ServerIP>().m_vector2.Add(vecTemp); 
                    }
                    
                    //m_servers[i].GetComponent<ServerIP>().mapType=data[i]
                    //m_servers[i].transform.GetChild(0).GetComponent<Text>().text = "服务器" + i + "(" + ip[i] + ")";  //TODO:显示到服务器列表的内容
                }
            }
            else
            {
                ShowTips("没有扫描到服务器！");
            }
        }

        print("扫描完毕！扫描到" + m_discovery.broadcastsReceived.Count + "个服务器！");
        StopUDP();
    }


    //创建主机方法
    public void CreateHost(MapType mapTypeInfo)
    {
        //Debug.Log("networkAddress:"+GetComponent<LobbyManager>().networkAddress);
        //创建服务器
        m_client = GetComponent<LobbyManager>().StartHost();

       
        if (m_client == null)
        {
            ShowTips("创建主机失败......！");
        }
        else
        {
            //创建成功 设置UI显示 TODO
            mapInfo = mapTypeInfo;
            CreatRoomGo.SetActive(false);
            RoomGo.SetActive(true);
            ActiveRoomButton(false);

            RandomNum();
            m_discovery.broadcastData = mapInfo.ToString();
            for (int i = 0; i < m_vecotr2.Count; i++)
            {
                m_discovery.broadcastData += "_" + m_vecotr2[i];
            }

            SendUDP();
        }
    }

    public void ShowTips(string strValue) { 

        tipText.text = strValue;
        tipsInfo.SetActive(true);

    }


    void TipsButton() {

        tipsInfo.SetActive(false); 

    }

    public void JoinClient(string ip)
    { 
        //获取地址
        m_manager.networkAddress = ip;

        //开始连接
        m_client = GetComponent<LobbyManager>().StartClient();

        //实时判断是否连接上服务器
        InvokeRepeating("PanDuan", 0, 0.05f);

        //设置超时时间
        Invoke("TimeChao", 10f);
     
    }



    void Quit()
    {
        //停止服务器
        GetComponent<LobbyManager>().StopHost();
        GetComponent<LobbyManager>().StopClient(); 

        //关闭定时器
        CancelInvoke("PanDuan");
        CancelInvoke("TimeChao");
    }


    //判断是否连接上服务器
    void PanDuan()
    {
        //是否连接到服务器
        if (m_client.isConnected)
        {
            
            //关闭两个定时器
            CancelInvoke("PanDuan");
            CancelInvoke("TimeChao");
        }
    }


    //超时方法
    void TimeChao()
    {
        //先把连接停止
        GetComponent<LobbyManager>().StopClient();

        //显示连接超时
        Debug.Log("连接超时！");
        ShowTips("连接超时！");
        //关闭定时器
        CancelInvoke("PanDuan");
    }


    //隐藏所有UI
    public void AllUIFalse()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    private void RandomNum() {

        for (int i = 0; i < FloorNum; i++)
        {
            int SpecialLengthPos = UnityEngine.Random.Range(-1 * length / 2, length / 2);
            int SpecialWidthPos = UnityEngine.Random.Range(-1 * width / 2, width / 2);

            Vector2 temp = new Vector2(SpecialLengthPos, SpecialWidthPos);
            m_vecotr2.Add(temp);
        } 
        
    }

    void ActiveRoomButton(bool isShow) {

        createHost.gameObject.SetActive(isShow);
        joinButton.gameObject.SetActive(isShow);
        nameInput.gameObject.SetActive(isShow);


    }

}
