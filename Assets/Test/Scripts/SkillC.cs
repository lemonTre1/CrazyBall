using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillC : MonoBehaviour {

    Rigidbody m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponentInParent<Rigidbody>();
    }

    public void Show()
    {
        Vector3 position;
        if (m_rigidbody.velocity == Vector3.zero)
            position = Vector3.forward * 10;
        else
            position = new Vector3(m_rigidbody.velocity.x, 0, m_rigidbody.velocity.z).normalized * 20;
        transform.parent.position += position;
    }

    public void Reset()
    {
        
    }
}
