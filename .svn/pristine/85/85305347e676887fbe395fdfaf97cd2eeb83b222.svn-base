using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BufferController : MonoBehaviour {
    public float fBufferRate;
    public Image ImageMask;

    float StartValue = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartValue += Time.deltaTime * fBufferRate;
     

        if (StartValue>1f)
        {
            Destroy(this.gameObject);
        }

        ImageMask.fillAmount = StartValue;
    }
}
