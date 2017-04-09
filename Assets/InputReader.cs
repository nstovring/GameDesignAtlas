using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour {

    PlatformInputController myInputController;

    List<Vector3> RecInput;
    Vector3 StartPosition;
    // Use this for initialization
    void Start () {
        myInputController = GetComponent<PlatformInputController>();
        RecInput = new List<Vector3>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(RecInput.Count > 0)
        {
            myInputController.HSpeed = RecInput[0].x;
            bool jumping = (RecInput[0].y != 0) ? true : false;
            myInputController.motor.inputJump = jumping;
            RecInput.RemoveAt(0);
        }else
        {
            myInputController.HSpeed = 0;
            myInputController.motor.inputJump = false;
        }
	}

    public void SetRecInput(List<Vector3> recInput, Vector3 startPos)
    {
        RecInput = recInput;
        StartPosition = startPos;
        transform.position = StartPosition;
    }
}
