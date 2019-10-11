using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

     
    public GameObject parent;
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }


    void Start () {

        //m_multiplayer_button.onClick.AddListener(EnterMultiPlayer);
        DontDestroyOnLoad(this.gameObject);
    }
	
	void Update () {
		
	}
     

}
