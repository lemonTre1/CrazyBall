using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BufferType
{
    SpeedUp,
    MassUp
}

public class UIManager : MonoBehaviour {
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Person Info")]
    public Text TextName;

    [Header("Buffer")]
    public Transform tfBufferParent;
    public GameObject gSpeedUpPrefab;
    public GameObject gMassUpPrefab;



	void Awake () {
        if (instance==null)
        {
            instance = this;
        } 
    }

    private void Start()
    {
        TextName.text = LobbyUI._Instance.m_name;
    }

    public void PrintName (string strName) {
        TextName.text = strName;

    }

    public void AddBuffer(BufferType bt)
    {
        switch (bt)
        {
            case BufferType.SpeedUp:
                initBuffer(gSpeedUpPrefab);
                break;
            case BufferType.MassUp:
                initBuffer(gMassUpPrefab);
                break;
            default:
                break;
        }
    }

    void initBuffer(GameObject gBuffer)
    {
        GameObject.Instantiate(gBuffer,tfBufferParent);
    }
}
