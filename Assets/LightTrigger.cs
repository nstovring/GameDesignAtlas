using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour {
    public Vector3 lightOffset;
    public Vector3 lightRotation;

    public Vector3 defaultlightOffset;
    public Vector3 defaultlightRotation;

    public Color colorA;
    public Color colorB;

    public Light light;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {

        //cameraMover.newSpeed = 0.2f;
        //cameraMover.offset = cameraOffset;
        //light.transform.rotation = Quaternion.Euler(lightRotation);
        StartCoroutine(RotationLerp());
        StartCoroutine(ColorLerp());
    }

    public void OnTriggerExit(Collider other)
    {
        //cameraMover.offset = defaultCameraOffset;
        //cameraMover.rotationOffset = defaultCameraRotation;
        //StartCoroutine(DelayedSpeedReset());
    }

    IEnumerator ColorLerp()
    {
        Quaternion quatLightDir = Quaternion.Euler(lightRotation);
        Color currentColor = colorA;
        while (currentColor != colorB)
        {
            currentColor = Color.Lerp(currentColor, colorB, 0.005f);
            light.color = currentColor;
            if (currentColor.maxColorComponent - colorB.maxColorComponent <= 0.1f)
                light.color = colorB;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator RotationLerp()
    {
        Quaternion quatLightDir = Quaternion.Euler(lightRotation);
        while (Quaternion.Angle(light.transform.rotation, quatLightDir) > 0.1f)
        {

            light.transform.rotation = Quaternion.Lerp(light.transform.rotation, quatLightDir, 0.01f);
            if (Quaternion.Angle(light.transform.rotation, quatLightDir) <= 0.1f)
                light.transform.rotation = quatLightDir;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
