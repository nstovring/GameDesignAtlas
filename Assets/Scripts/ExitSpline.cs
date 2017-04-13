using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpline : MonoBehaviour, IInteractable {

    private GameObject player;


    public void Interact()
    {

        player = GameObject.FindGameObjectsWithTag("Player")[0];

        player.GetComponent<PlatformInputController>().progress = 1.1f;


    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
