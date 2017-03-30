using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicElevator : MonoBehaviour, ILinkable {

    public Transform LevitationField;
    public void Activate()
    {
        Debug.Log("Activate Field");
        LevitationField.gameObject.SetActive(true);
        //LevitationField.transform.GetComponent<Renderer>().enabled = true;
        //LevitationField.transform.GetComponent<Renderer>().enabled = false;

    }

    public void Deactivate()
    {
        LevitationField.gameObject.SetActive(false);
    }     
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
