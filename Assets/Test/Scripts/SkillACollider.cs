using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillACollider : MonoBehaviour {

    public Transform _parent;
    void OnCollisionEnter(Collision collisionInfo)
    {
   
        GameObject obj = collisionInfo.collider.gameObject;
        if (obj.GetComponent<Rigidbody>())
        {
            GameObject player = GetComponentInParent<PlayerController>().gameObject;
            Vector3 forward = (obj.transform.position - transform.position).normalized;
            obj.GetComponent<Rigidbody>().AddForce(forward * 10000 * player.GetComponent<Rigidbody>().mass);

            if(obj.transform.CompareTag("SkillA"))
              obj.transform.GetComponent<PlayerData>().Attacker = player.transform.GetComponentInParent<PlayerData>().PlayerNum;
            else
                if(obj.transform.CompareTag("Player"))
                obj.transform.GetComponent<PlayerData>().Attacker = player.transform.GetComponent<PlayerData>().PlayerNum;
        }
 
    }
}
