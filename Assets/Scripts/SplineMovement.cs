using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMovement : MonoBehaviour {

    public float HSpeed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HSpeed = Input.GetAxis("Horizontal");
	}

    
}
