using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeightTrigger : MonoBehaviour {
    public ElevatorScript elevator;
    public float elevatorHeightEntrance;
    float delay;
    public CameraBlinding cameraBlindScript;
    public List<GameObject> coliders;
    IEnumerator Start()
    {
        elevator.upperLimit = elevatorHeightEntrance;
        while(elevator.transform.localPosition.y < elevatorHeightEntrance)
        {
            if (Mathf.Abs(elevator.transform.localPosition.y - elevatorHeightEntrance) < 0.1) break;
            yield return new WaitForSeconds(delay);
        }
        elevator.StartElevator = false;
    }
    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player")
        {
            elevator.StartElevator = true;
            elevator.upperLimit = elevator.newUpperLimit;
            foreach(GameObject obj in coliders)
            {
                obj.active = true;
            }
            transform.GetComponent<BoxCollider>().size += new Vector3(0.5f, 0, 0);
            StartCoroutine(GoToWhiteAndRestart());
        }
        
    }
    void OnTriggerExit(Collider Other)
    {
        if (Other.tag == "Player")
        {
            elevator.upperLimit = elevator.oldUpperLimit;
            StopCoroutine(GoToWhiteAndRestart());
            cameraBlindScript.BlendWeight = 0;
        }

    }
    IEnumerator GoToWhiteAndRestart()
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            cameraBlindScript.BlendWeight = Mathf.Lerp(cameraBlindScript.BlendWeight, 1, progress);
            progress += increment *Time.deltaTime;

            if (cameraBlindScript.BlendWeight > 0.85)
            {
                cameraBlindScript.BlendWeight = 1;
            }
            yield return new WaitForSeconds(smoothness);
        }
        SceneManager.LoadScene(0);
    }

    float duration = 20; // This will be your time in seconds.
    float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
}
