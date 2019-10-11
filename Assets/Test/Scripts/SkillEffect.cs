using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SkillEffect : NetworkBehaviour
{
    GameObject SkillA;
    GameObject SkillB;
    GameObject SkillC;
    GameObject AttackEffect;
    Rigidbody m_rigidbody;
    PlayerController m_PlayerController;

    void Start()
    {
        SkillA = transform.Find("SkillA").gameObject;
        SkillB = transform.Find("SkillB").gameObject;
        SkillC = transform.Find("SkillC").gameObject;
        m_PlayerController = GetComponent<PlayerController>();
        m_rigidbody = GetComponent<Rigidbody>();
        AttackEffect = transform.Find("Sparks").gameObject;
        AttackEffect.SetActive(false);
    }

    [Command]
    public void CmdAddForce(Vector3 power)
    {
        RpcAddForce(power);
    }

    [ClientRpc]
    public void RpcAddForce(Vector3 power)
    {
        m_rigidbody.AddForce(power);
    }


    [Command]
    public void CmdSkillA(bool isOn)
    {
        RpcSkillA(isOn);
    }


    [ClientRpc]
    public void RpcSkillA(bool isOn)
    {
        SkillA skillA = GetComponentInChildren<SkillA>();
        if (isOn)
            skillA.Show();
        else
            skillA.Reset();
    }

    [Command]
    public void CmdSkillB()
    {
        RpcSkillB();
    }


    [ClientRpc]
    public void RpcSkillB()
    {
        SkillB skillB = GetComponentInChildren<SkillB>();
        skillB.Show();
    }

    [Command]
    public void CmdSkillC()
    {
        RpcSkillC();
    }


    [ClientRpc]
    public void RpcSkillC()
    {
        SkillC skillC = GetComponentInChildren<SkillC>();
        skillC.Show();
    }

    public void UpdateAttribute(bool isTrigger)
    {
        CmdUpdateAttribute(m_PlayerController.m_PlayerAttributes.ExpansionMultiple, m_PlayerController.m_PlayerAttributes.StrengthValue, isTrigger);
    }


    [Command]
    public void CmdUpdateAttribute(float expan, int str, bool isTrigger)
    {
        RpcUpdateAttribute(expan, str, isTrigger);
    }

    [ClientRpc]
    public void RpcUpdateAttribute(float expan, int str, bool isTrigger)
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * expan;
        if (m_rigidbody==null)
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
        m_rigidbody.mass = str;
        AttackEffect.gameObject.SetActive(isTrigger);
    }

}
