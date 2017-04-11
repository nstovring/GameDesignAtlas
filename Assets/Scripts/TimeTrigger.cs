using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour {

    public Transform pastObjects;
    public Transform presentObjects;

    public void OnTriggerEnter(Collider other)
    {

        //cameraMover.newSpeed = 0.2f;
        //cameraMover.offset = cameraOffset;
        //light.transform.rotation = Quaternion.Euler(lightRotation);

        pastObjects.gameObject.SetActive(false);
        presentObjects.gameObject.SetActive(true);
    }
}
