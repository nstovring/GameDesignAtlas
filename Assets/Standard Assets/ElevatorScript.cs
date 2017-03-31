using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorScript : MonoBehaviour {



    public bool StartElevator;

    [Header("Elevator Settings")]
    public float ElevatorSpeed;
    public float upperLimit;
    public float lowerLimit;

    private bool UpwardsOrDownwards;
        
    [Header("Elevator Direction, set false for x axis, and true for y axis")]
    public bool XorY;

    void Start()
    {
        //Initialization();
        UpwardsOrDownwards = false;
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

        if (transform.position.x <= lowerLimit)
        {
            UpwardsOrDownwards = true;
        }

        if (transform.position.x >= upperLimit)
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

        if (transform.position.y <= lowerLimit)
        {
            UpwardsOrDownwards = true;
        }

        if (transform.position.y >= upperLimit)
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
}
