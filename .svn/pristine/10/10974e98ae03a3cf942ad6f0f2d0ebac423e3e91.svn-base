using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillB : MonoBehaviour {

    Rigidbody m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponentInParent<Rigidbody>();
    }

    public void Show()
    {
        if (m_rigidbody.velocity == Vector3.zero)
            return;
        Vector3 position = new Vector3(m_rigidbody.velocity.x, -30, m_rigidbody.velocity.z).normalized * 5;
        GameObject cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().mass = 10;
        cube.transform.localScale = Vector3.one * 3;
        cube.transform.position = transform.position - position;
        StartCoroutine(CubleExplode(cube));

    }

    public void Reset()
    {
        
    }

    IEnumerator CubleExplode(GameObject cube)
    {
        yield return new WaitForSeconds(2);
        cube.transform.localScale = Vector3.one;
        for (int i = -2; i < 2; i++)
        {
            for (int j = -2; j < 2; j++)
            {
                for (int k = -2; k < 2; k++)
                {
                    if (i == 0 && j == 0 && k == 0)
                    {
                        break;
                    }
                    GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    c.AddComponent<Rigidbody>();
                    c.transform.SetParent(cube.transform);
                    c.transform.localPosition = new Vector3(i, j, k);
                    c.GetComponent<Rigidbody>().mass = 10;
                    c.transform.SetParent(null);
                }
            }
        }


        Vector3 explosionPos = cube.transform.position + Vector3.down;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 20);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(20000, explosionPos, 30);
        }

    }
}
