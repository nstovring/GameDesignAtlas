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
        StartCoroutine(AmbientLerp());
    }

    public void OnTriggerExit(Collider other)
    {
        //cameraMover.offset = defaultCameraOffset;
        //cameraMover.rotationOffset = defaultCameraRotation;
        //StartCoroutine(DelayedSpeedReset());
    }
    public float newAmbientIntensity = 0;
    IEnumerator AmbientLerp()
    {
        float ambientIntensity = RenderSettings.ambientIntensity;

        while (ambientIntensity != newAmbientIntensity)
        {
            ambientIntensity = Mathf.Lerp(ambientIntensity, newAmbientIntensity, 0.01f);
            if (Mathf.Abs(ambientIntensity-newAmbientIntensity) <= 0.1f)
            {
                ambientIntensity = newAmbientIntensity;
                //RenderSettings.ambientIntensity = newAmbientIntensity;
            }
           RenderSettings.ambientIntensity = ambientIntensity;
           yield return new WaitForSeconds(0.01f);
        }
    }


        IEnumerator ColorLerp()
    {
        Quaternion quatLightDir = Quaternion.Euler(lightRotation);
        Color currentColor = light.color;
        Vector3 currentColorVec = new Vector3(currentColor.r, currentColor.g, currentColor.b);
        Vector3 NewColorVector = new Vector3(colorB.r, colorB.g, colorB.b);

        while (currentColor != colorB)
        {
            currentColor = Color.Lerp(currentColor, colorB, 0.005f);
            currentColorVec = new Vector3(currentColor.r, currentColor.g, currentColor.b);
            if (Vector3.Distance(currentColorVec, NewColorVector) <= 0.01f)
            {
                currentColor = colorB;
            }
            light.color = currentColor;

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
