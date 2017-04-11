using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCameraDummy : MonoBehaviour {


    public Transform lookAt;
    public float HozSpeed;
    public float VerSpeed;


    // Use this for initialization
    void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.D)) {
            print("D");

            this.transform.RotateAround(lookAt.position, Vector3.up, (HozSpeed * Time.deltaTime) * -1);

            Vector3 tmp = this.transform.position;
            tmp.y += VerSpeed * Time.deltaTime;

            this.transform.position = tmp;

        }
        if (Input.GetKey(KeyCode.A))
        {
            print("A");

            this.transform.RotateAround(lookAt.position, Vector3.down, (HozSpeed * Time.deltaTime) * -1);

            Vector3 tmp = this.transform.position;
            tmp.y -= VerSpeed * Time.deltaTime;

            this.transform.position = tmp;

        }


        //this.transform.LookAt(lookAt);
    }
}
