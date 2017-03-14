using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShifting : MonoBehaviour {

	private bool presentTime = true;

	// Use this for initialization
	void Start () {

		//Test to see if we can access the values of the Level Manager
		Debug.Log(LevelManager.lm.test);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("e")) {
			presentTime = !presentTime;
			//Debug.Log("Time shifting! Present time = " + presentTime);
			changeTime (presentTime);
		}
	}

	public void changeTime (bool time) {
		LevelManager.lm.changeTime (time);
	}


}
