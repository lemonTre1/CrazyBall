﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour {

    public GameObject LeftJoyStick = null;
    public GameObject RightJoyStick = null;
    public GameObject PersonInfoPanel = null;
    public GameObject EnergyPanel = null;
    public GameObject SkillPanel = null;

    public static ReferenceManager instance;

    private void Awake()
    {
        instance = this;
    } 

}