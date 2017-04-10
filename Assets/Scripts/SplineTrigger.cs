using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrigger : MonoBehaviour, IInteractable {

    public BezierSpline spline;

    private GameObject player;

    public void Interact()
    {

        if(spline != null)
        {
            Debug.Log("give player spline");
            player = GameObject.FindGameObjectsWithTag("Player")[0];

            player.GetComponent<PlatformInputController>().directionVector = new Vector3(0f, 0f, 0f);
            player.GetComponent<PlatformInputController>().velocityVector = new Vector3(0f, 0f, 0f);

            player.GetComponent<PlatformInputController>().spline = spline;
            player.GetComponent<PlatformInputController>().setSplineLength();
            player.GetComponent<PlatformInputController>().OnPath = true;

            Camera.main.GetComponent<CameraMover>().SplineMovement = true;

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
