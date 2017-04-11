using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWay : MonoBehaviour, ILinkable {
    public Animator animator;
    public bool open = false;
    public void Activate()
    {
        open = true;
        animator.SetBool("Open", true);
    }

    public void Deactivate()
    {
        open = false;
        animator.SetBool("Open", false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
