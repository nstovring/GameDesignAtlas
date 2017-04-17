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
        while(cameraBlindScript.BlendWeight < 1)
        {
            cameraBlindScript.BlendWeight = Mathf.Lerp(cameraBlindScript.BlendWeight, 1, 0.007f);
            if(cameraBlindScript.BlendWeight > 0.85)
            {
                cameraBlindScript.BlendWeight = 1;
            }
            yield return new WaitForSeconds(delay);
        }
        SceneManager.LoadScene(0);
    }
}
