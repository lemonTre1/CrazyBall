using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerSpeed{

    first,
    Second
}

//[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    //private Rigidbody _rigidbody;
    //public bool bSpeed = false;
    public int Scroe=0;
    public float Force;
    /// <summary>
    /// 移动类型
    /// </summary>
    public PlayerSpeed m_speed;
    /// <summary>
    /// 玩家编号
    /// </summary>
    public int PlayerNum = 1;
    /// <summary>
    /// 攻击者编号
    /// </summary>
    public int Attacker = -1;
    // Start is called before the first frame update
    void Start()
    {
        //_rigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(bSpeed)
        //Speed();
    }
    public void Init(int _index)
    {
        PlayerNum = _index;
        Clear();
    }
    public void SetScore()
    {
        Scroe++;
        Debug.Log("玩家"+ PlayerNum+"Score="+ Scroe);
    }
    public void Clear()
    {
        Attacker = -1;
    }
    //private Vector3 Movement()
    //{
    //   float hor = Input.GetAxis("Horizontal");
    //   float ver = Input.GetAxis("Vertical");
    //   Vector3 movement = transform.parent.forward * ver + transform.parent.right * hor;
    //   return movement;
    //}
    //private void Speed()
    //{
    //    switch (m_speed)
    //    {
    //        case PlayerSpeed.first:
    //            //first
    //            _rigidbody.MovePosition(transform.position + Movement() * 0.2f);
    //            break;
    //        case PlayerSpeed.Second:
    //            //Second
    //            _rigidbody.AddForce(Movement() * Force*1f);
    //            break;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //ContactPoint contact = collision.contacts[0];
            //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            //Vector3 pos = contact.point;    //这个就是碰撞点
            //Vector3 verForce = collision.transform.position - pos;
            //collision.rigidbody.AddForce(verForce * 1000 * Force, ForceMode.Force);
            Attacker = collision.transform.GetComponent<Player>().PlayerNum;
        }
    }
}
