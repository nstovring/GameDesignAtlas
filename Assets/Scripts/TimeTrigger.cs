using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : MonoBehaviour {

    public Transform pastObjects;
    public Transform presentObjects;

    public Transform Vegetation;

    public void OnTriggerEnter(Collider other)
    {

        //cameraMover.newSpeed = 0.2f;
        //cameraMover.offset = cameraOffset;
        //light.transform.rotation = Quaternion.Euler(lightRotation);

        pastObjects.gameObject.SetActive(false);
        presentObjects.gameObject.SetActive(true);
        if(Vegetation != null)
        {
            StartCoroutine(GrowVegetation());
        }
    }
    float vegetationScale = 0;
    IEnumerator GrowVegetation()
    {
        while(vegetationScale < 1)
        {
            vegetationScale = Mathf.Lerp(vegetationScale, 1, 0.05f);
            Vegetation.localScale =  new Vector3(Vegetation.localScale.x, vegetationScale, Vegetation.localScale.z);

            if(vegetationScale > 0.95f)
            {
                vegetationScale = 1;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
