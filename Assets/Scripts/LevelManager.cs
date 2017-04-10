using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    float delay;
	//Used to access varaibles of this class
	public static LevelManager lm;

	//Test variable
	public int test = 2;

	public GameObject presentObjects;
	public GameObject pastObjects;
    public GameObject alltimeObjects;
    public CameraBlinding blindingShader;
    public int keyscollected = 0;
    public int spherescollected = 0;
    public delegate void TimeShiftHandler(bool time);
    public event TimeShiftHandler TimeShift;

    public TimeShifting timeShiftingClass;
    // Use this for initialization
    IEnumerator Start () {
		lm = this;
		//Set present objects visible only
		/*foreach (Transform child in presentObjects.transform) {
			Debug.Log ("Child: " + child.name + " becoming visible");
			//Make visible
			presentObjects.SetActive(true);
		}
		foreach (Transform child in pastObjects.transform) {
			Debug.Log ("Child: " + child.name + " becoming invisible");
			//Make invisible
			pastObjects.SetActive(false);
		}*/
        
        try {
            blindingShader = GetComponent<CameraBlinding>();
        }
        catch
        {
            
        }
        yield return new WaitForSeconds(delay+ delay);
      //  TimeShift(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetObjectivesGathered(ObjectiveItem.ObjectiveItemType type)
    {
        if (type == ObjectiveItem.ObjectiveItemType.Key)
            keyscollected += 1;
        if (type == ObjectiveItem.ObjectiveItemType.PowerCore)
            spherescollected += 1;
        if (type == ObjectiveItem.ObjectiveItemType.Artifact)
            timeShiftingClass.CanPlayerTimeShift = true;
    }
	//Receives a 'true' from TimeShifting class if shifting to present, else false
	public void changeTime(bool present) {
        //If changing to present
        TimeShift(present);
        if (false)
        {
            if (present)
            {
                Debug.Log("Changing to present time");

                //Change objects
                foreach (Transform child in presentObjects.transform)
                {
                    Debug.Log("Child: " + child.name + " becoming visible");
                    //Make visible
                    presentObjects.SetActive(true);


                }
                foreach (Transform child in pastObjects.transform)
                {
                    Debug.Log("Child: " + child.name + " becoming invisible");
                    //Make invisible
                    pastObjects.SetActive(false);
                }

                foreach (Transform child in alltimeObjects.transform)
                {
                    Renderer childRenderer = child.transform.GetComponent<Renderer>();
                    if (childRenderer != null)
                    {
                        childRenderer.material.SetFloat("_Blend", 0.8f);
                    }
                    else
                    {
                        child.transform.GetComponentInChildren<Renderer>().material.SetFloat("_Blend", 0.8f);
                    }
                }

                //Change camera shader

                //Notify TimeShader
            }
            //If changing to past
            else
            {
                Debug.Log("Changing to past time");

                //Change objects
                foreach (Transform child in presentObjects.transform)
                {
                    Debug.Log("Child: " + child.name + " becoming invisible");
                    //Make visible
                    presentObjects.SetActive(false);
                }
                foreach (Transform child in pastObjects.transform)
                {
                    Debug.Log("Child: " + child.name + " becoming visible");
                    //Make invisible
                    pastObjects.SetActive(true);
                }


                foreach (Transform child in alltimeObjects.transform)
                {
                    Renderer childRenderer = child.transform.GetComponent<Renderer>();
                    if (childRenderer != null)
                    {
                        childRenderer.material.SetFloat("_Blend", .0f);
                    }
                    else
                    {
                        child.transform.GetComponentInChildren<Renderer>().material.SetFloat("_Blend", .0f);
                    }

                }
                //Change camera shader

                //Notify TimeShader
            }
        }
	}

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
