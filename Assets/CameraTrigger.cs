using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour{
    public Vector3 cameraOffset;
    public Vector3 cameraRotation;

    public Vector3 defaultCameraOffset;
    public Vector3 defaultCameraRotation;


    private CameraMover cameraMover;

    public void Interact()
    {
        cameraMover.offset = cameraOffset;
        cameraMover.rotationOffset = cameraRotation;
    }

    // Use this for initialization
    void Start () {
        cameraMover = Camera.main.GetComponent<CameraMover>();
    }

    public void OnTriggerEnter(Collider other)
    {
        cameraMover.newSpeed = 0.2f;
        cameraMover.offset = cameraOffset;
        cameraMover.rotationOffset = cameraRotation;
    }

    public void OnTriggerExit(Collider other)
    {
        cameraMover.offset = defaultCameraOffset;
        cameraMover.rotationOffset = defaultCameraRotation;
        StartCoroutine(DelayedSpeedReset());
    }

    IEnumerator DelayedSpeedReset()
    {
        while(cameraMover.newSpeed != 1)
        {
            cameraMover.newSpeed = Mathf.Lerp(cameraMover.newSpeed, 1, 0.2f);
            if (cameraMover.newSpeed >= 0.95)
                cameraMover.newSpeed = 1;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
