using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicElevator : MonoBehaviour, ILinkable {

    public Collider LevitationField;
    public void Activate()
    {
        LevitationField.transform.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
