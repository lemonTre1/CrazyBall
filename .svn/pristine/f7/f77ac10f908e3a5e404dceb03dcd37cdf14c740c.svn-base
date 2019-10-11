using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterMovement : NetworkBehaviour {

    [Header("Movement Attribute")]
    public float speed = 5;
    public float rotAngle = 0;
    public float turnSmoothTime = 0.2f;
    public Vector3 forward;
    float turnnSmoothVelocity;
    public float speedrate = 1;
    [Header("Reference")]
    CharacterController controller;
    public Rigidbody rig;
    public float inputHorizontal;
    public float inputVertical;
   
    [Header("Body parts reference")]

    public GameObject upperBody;
    public GameObject lowerBody;
    public GameObject m_inpul_left;
    public GameObject m_inpul_Right;

    [Header("Platform")]
    public bool PC = true;

    public override void OnStartServer()
    {
        base.OnStartServer();
    }


    //当自己的角色被创建的时候
    public override void OnStartLocalPlayer()
    {
        //关闭所有UI显示
        LobbyUI._Instance.AllUIFalse();
        transform.position = new Vector3(transform.position.x, transform.position.y+2, transform.position.z);
    }


    void Start () {
         

        controller = GetComponent<CharacterController>();
        rig = GetComponent<Rigidbody>();

        m_inpul_left = ReferenceManager.instance.LeftJoyStick;
        m_inpul_Right= ReferenceManager.instance.RightJoyStick;
     }

    //public override void OnStartLocalPlayer()
    //{
    //    this.gameObject.name = "LocalPlayer";
    //}
     
    void Update () {

        if (!isLocalPlayer)
            return;

        RpcMove();
    }

     
   

    //[Command]
    //void CmdMove()
    //{
    //    RpcMove();
    //}

    //[ClientRpc]
    void RpcMove()
    {
        float fHori = Input.GetAxis("Horizontal");
        float fVerti = Input.GetAxis("Vertical");
        rig.AddForce(new Vector3(fHori, 0, fVerti) * speedrate);
        speed = rig.velocity.magnitude;

        inputHorizontal = Input.GetAxisRaw("Vertical");

        MobileInputController mic = m_inpul_left.GetComponent<MobileInputController>();
        rig.AddForce(new Vector3(mic.Coordinate().x, 0, mic.Coordinate().y) * speedrate);

        Vector3 from = new Vector3(0f, 0f, 1f);
        Vector3 to = new Vector3(m_inpul_Right.GetComponent<MobileInputController>().Horizontal, 0f, m_inpul_Right.GetComponent<MobileInputController>().Vertical);

        if (m_inpul_Right.GetComponent<MobileInputController>().Horizontal != 0 && m_inpul_Right.GetComponent<MobileInputController>().Vertical != 0)
        {
            float angle = Vector3.SignedAngle(from, to, Vector3.up);
            rotAngle = angle;

            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnnSmoothVelocity, turnSmoothTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            CmdSelfGO(collision.contacts[0].point);
            CmdOtherGO(collision.contacts[0].point,collision.gameObject);
        }

    }

    [Command]    
    void CmdSelfGO(Vector3 Point)
    {

        RpcAddForce(Point);
    }

    [ClientRpc]
    void RpcAddForce(Vector3 collision)
    {
        //CharacterMovement cm_collsion = go.GetComponent<CharacterMovement>();
        //if (cm_collsion == null)
        //{
        //    Debug.LogError("[ERROR] : failed call function ---- OnCollisionEnter!");
        //    return;
        //}
        Vector3 vec3Force = transform.position - collision;

        if (rig==null)
        {
            rig = GetComponent<Rigidbody>();
        }
        rig.AddForce(vec3Force.normalized*1000*rig.mass);
    }

    [Command]
    void CmdOtherGO(Vector3 Point,GameObject gOther)
    {

        RpcOtherAddForce(Point, gOther);
    }

    [ClientRpc]
    void RpcOtherAddForce(Vector3 Point,GameObject gOther)
    {
        //CharacterMovement cm_collsion = go.GetComponent<CharacterMovement>();
        //if (cm_collsion == null)
        //{
        //    Debug.LogError("[ERROR] : failed call function ---- OnCollisionEnter!");
        //    return;
        //}
        Vector3 vec3Force = gOther.transform.position - Point;

        gOther.GetComponent<Rigidbody>().AddForce(vec3Force.normalized * 1000 * rig.mass);
    }
}
