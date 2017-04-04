using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorder : MonoBehaviour {

    [HideInInspector]
    List<Vector3> RecInput;
    List<float> RecYspeed;

    public Vector3 pastPos;
    // Use this for initialization
    void Start () {
        ResetInput();
	}
	
    public void RecordInput(Vector3 curInput)
    {
        RecInput.Add(curInput);
    }

    public void ResetInput()
    {
        RecInput = new List<Vector3>();
        pastPos = transform.position;
    }

    public List<Vector3> GetInput()
    {
        return RecInput;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
