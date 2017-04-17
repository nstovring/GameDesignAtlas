using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorScript : MonoBehaviour, ILinkable {



    public bool StartElevator;
    public bool SetHeightOnActivation;
    public float newUpperLimit;

    [Header("Elevator Settings")]
    public float ElevatorSpeed;
    public float upperLimit;
    public float lowerLimit;
    public float oldUpperLimit;
    private bool UpwardsOrDownwards;
        
    [Header("Elevator Direction, set false for x axis, and true for y axis")]
    public bool XorY;

    public List<Renderer> elevatorLights;
    private Color elevatorLightColor;
    public Transform heightTrigger;

    void Start()
    {
        //Initialization();
        if (elevatorLights.Count == 0)
        {
            Debug.Log("No Renderer attatched!");
        }
        else
        {
            elevatorLightColor = elevatorLights[0].material.GetColor("_EmissionColor");
        }
        UpwardsOrDownwards = false;
        oldUpperLimit = upperLimit;

        SetState(StartElevator);
        //if (StartElevator)
        //{
        //    Activate();
        //}
        //else
        //{
        //    Deactivate();
        //}
    }

    void ChangeLights(bool lightState)
    {
        if (lightState)
        {
            foreach (var item in elevatorLights)
            {
                item.material.SetColor("_EmissionColor", elevatorLightColor);
            }
        }
        else
        {
            foreach (var item in elevatorLights)
            {
                item.material.SetColor("_EmissionColor", Color.black);
            }
        }
    }

    void Update () {

        StatusChecker();
	}

    private void Initialization() {
        if (XorY == true)
        {
            upperLimit += this.transform.position.y;
            lowerLimit += this.transform.position.y;
        }
        if (XorY == false)
        {
            upperLimit += this.transform.position.x;
            lowerLimit += this.transform.position.x;
        }

    }

    private void StatusChecker() {
        if (Input.GetKey("p"))
        {
            StartElevator = true;
        }

        if (StartElevator == true)
        {

            if (XorY == true)
            {
                UpDown();
            }
            if (XorY == false)
            {
                LeftRight();
            }
        }
    }

    private void LeftRight() {

        if (transform.localPosition.x <= lowerLimit)
        {
            UpwardsOrDownwards = true;
        }

        if (transform.localPosition.x >= upperLimit)
        {
            UpwardsOrDownwards = false;
        }

        if (UpwardsOrDownwards == false)
        {
            Vector3 tmp = this.transform.position;
            tmp.x -= ElevatorSpeed * Time.deltaTime;
            this.transform.position = tmp;
        }

        if (UpwardsOrDownwards == true)
        {
            Vector3 tmp = this.transform.position;
            tmp.x += ElevatorSpeed * Time.deltaTime;
            this.transform.position = tmp;
        }
    }

    private void UpDown() {

        if (transform.localPosition.y <= lowerLimit)
        {
            UpwardsOrDownwards = true;
        }

        if (transform.localPosition.y >= upperLimit)
        {
            UpwardsOrDownwards = false;
        }

        if (UpwardsOrDownwards == false)
        {
            Vector3 tmp = this.transform.position;
            tmp.y -= ElevatorSpeed * Time.deltaTime;
            this.transform.position = tmp;
        }

        if (UpwardsOrDownwards == true)
        {
            Vector3 tmp = this.transform.position;
            tmp.y += ElevatorSpeed * Time.deltaTime;
            this.transform.position = tmp;
        }

    }

    public void SetState(bool state)
    {
        ChangeLights(state);
    }

    public void Activate()
    {

        if (!SetHeightOnActivation)
        {
            StartElevator = true;
            ///elevatorLight.material.SetColor("_EmissionColor", elevatorLightColor);
        }
        else{
            heightTrigger.gameObject.active = true;
            //upperLimit = newUpperLimit;
        }
    }

    public void Deactivate()
    {
        if (!SetHeightOnActivation)
        {
            StartElevator = false;
            //elevatorLight.material.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            heightTrigger.gameObject.active = false;
            //upperLimit = oldUpperLimit;
        }
    }
}
