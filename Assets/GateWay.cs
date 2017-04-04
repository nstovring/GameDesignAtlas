﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWay : MonoBehaviour, ILinkable {
    public Animator animator;
    public void Activate()
    {
        animator.SetBool("Open", true);
    }

    public void Deactivate()
    {
        animator.SetBool("Open", false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
