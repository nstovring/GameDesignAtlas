using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrigger : MonoBehaviour, IInteractable {

    public BezierSpline spline;

    public void Interact()
    {

        if(spline != null)
        {
            Debug.Log("give player spline");


        }
        else
        {
            Debug.Log("no spline, sorry");
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	
}
