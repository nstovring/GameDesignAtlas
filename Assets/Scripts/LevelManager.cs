using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	//Used to access varaibles of this class
	public static LevelManager lm;

	//Test variable
	public int test = 2;

	public GameObject presentObjects;
	public GameObject pastObjects;


	// Use this for initialization
	void Start () {
		lm = this;
		//Set present objects visible only
		foreach (Transform child in presentObjects.transform) {
			Debug.Log ("Child: " + child.name + " becoming visible");
			//Make visible
			presentObjects.SetActive(true);
		}
		foreach (Transform child in pastObjects.transform) {
			Debug.Log ("Child: " + child.name + " becoming invisible");
			//Make invisible
			pastObjects.SetActive(false);
		}
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
			foreach (Transform child in presentObjects.transform) {
				Debug.Log ("Child: " + child.name + " becoming visible");
				//Make visible
				presentObjects.SetActive(true);
			}
			foreach (Transform child in pastObjects.transform) {
				Debug.Log ("Child: " + child.name + " becoming invisible");
				//Make invisible
				pastObjects.SetActive(false);
			}

			//Change camera shader

			//Notify TimeShader
		} 
		//If changing to past
		else {
			Debug.Log ("Changing to past time");

			//Change objects
			foreach (Transform child in presentObjects.transform) {
				Debug.Log ("Child: " + child.name + " becoming invisible");
				//Make visible
				presentObjects.SetActive(false);
			}
			foreach (Transform child in pastObjects.transform) {
				Debug.Log ("Child: " + child.name + " becoming visible");
				//Make invisible
				pastObjects.SetActive(true);
			}

			//Change camera shader

			//Notify TimeShader
		}
	}
}
