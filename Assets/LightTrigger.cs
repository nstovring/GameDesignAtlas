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

    public float volumetricNoiseIntensity = 1;
    public bool heightFog = false;
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
        StartCoroutine(LerpColor());
        StartCoroutine(AmbientLerp());
        light.GetComponent<VolumetricLight>().HeightFog = heightFog;
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
        float currentVolumetricNoiseIntensity = light.GetComponent<VolumetricLight>().NoiseIntensity;
        while (ambientIntensity != newAmbientIntensity)
        {
            ambientIntensity = Mathf.Lerp(ambientIntensity, newAmbientIntensity, 0.01f);
            currentVolumetricNoiseIntensity = Mathf.Lerp(currentVolumetricNoiseIntensity, volumetricNoiseIntensity, 0.01f);
            if (Mathf.Abs(ambientIntensity-newAmbientIntensity) <= 0.1f)
            {
                ambientIntensity = newAmbientIntensity;
                currentVolumetricNoiseIntensity = volumetricNoiseIntensity;
                //RenderSettings.ambientIntensity = newAmbientIntensity;
            }
            light.GetComponent<VolumetricLight>().NoiseIntensity = currentVolumetricNoiseIntensity;
           RenderSettings.ambientIntensity = ambientIntensity;
           yield return new WaitForSeconds(0.01f);
        }
    }


    float duration = 5; // This will be your time in seconds.
    float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
    Color currentColor = Color.white; // This is the state of the color in the current interpolation.


    IEnumerator LerpColor()
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            currentColor = Color.Lerp(colorA,colorB, progress);
            progress += increment;
            light.color = currentColor;
            yield return new WaitForSeconds(smoothness);
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
