using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShifting : MonoBehaviour {

	private bool presentTime = true;
    public bool CanPlayerTimeShift = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CanPlayerTimeShift)
        {
        }
	}

	public void changeTime (bool time) {
        CameraBlinding blinding = LevelManager.lm.blindingShader;
        if (blinding.isActiveAndEnabled)
        {
            blinding.time = time;
            blinding.CallFlashScreen();
        }
        else
            LevelManager.lm.changeTime(time);
    }


}
