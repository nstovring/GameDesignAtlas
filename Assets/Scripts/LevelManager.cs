using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public static bool gameStarted;
    public Text startText;
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
            blindingShader.BlendWeight = 1;
        }
        catch
        {
            
        }
        yield return new WaitForSeconds(delay+ delay);

        while(blindingShader.BlendWeight > 0)
        {
            blindingShader.BlendWeight = Mathf.Lerp(blindingShader.BlendWeight, 0, 0.05f);
            yield return new WaitForSeconds(delay);
            if (blindingShader.BlendWeight < 0.01f)
            {
                blindingShader.BlendWeight = 0;
            }
        }
        //  TimeShift(true);
    }
	
	// Update is called once per frame
	void Update () {
        if(!gameStarted && Input.GetKeyDown(KeyCode.D))
        {
            startText.CrossFadeColor(Color.clear, 2, false, true);
            gameStarted = true;
            StartCoroutine(DelayedInstructions("Press \"Space\" To Jump", 4));
        }
	}

    public IEnumerator DelayedInstructions(string instructions, float delay)
    {
        yield return new WaitForSeconds(delay);
        startText.text = instructions;
        startText.CrossFadeColor(Color.white, delay, false, true);
        yield return new WaitForSeconds(delay);
        startText.CrossFadeColor(Color.clear, 2, false, true);
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
