using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	//Used to access varaibles of this class
	public static LevelManager lm;

	//Test variable
	public int test = 2;


	// Use this for initialization
	void Start () {
		lm = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Receives a 'true' from TimeShifting class if shifting to present, else false
	public void changeTime(bool present) {
		//If changing to present
		if (present) {
			Debug.Log ("Changing to present time");

			//Change objects

			//Change camera shader

			//Notify TimeShader
		} 
		//If changing to past
		else {
			Debug.Log ("Changing to past time");

			//Change objects

			//Change camera shader

			//Notify TimeShader
		}
	}
}
