using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoverAlternate : MonoBehaviour {

    public Transform target;

    public Vector3 offset = new Vector3(0, 4.14f, -10);
    public Vector3 rotationOffset = Vector3.zero;
    public Vector3 FirstArmOffset = Vector3.zero;

    public bool SplineMovement = false;
    
    
    public float HSpeed;
    public float distanceFromObject;
    public float speed = 1;
    public float newSpeed = 1;

    // Use this for initialization
    void Start()
    {
        //offset = transform.position;
        transform.LookAt(target);

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        
        FollowPlayer();

        
    }

   
  
    void FollowPlayer()
    {
        Vector3 newPosition = target.position;
        HSpeed = Input.GetAxis("Horizontal");

       
         if (SplineMovement)
        {
            newPosition += offset;


           

            if (HSpeed > 0.9f || HSpeed < -0.9f)
            {
                transform.position = (target.transform.position - (HSpeed * FirstArmOffset) * 10) + offset;
                transform.position = Vector3.Lerp(transform.position, (target.transform.position - (HSpeed * FirstArmOffset) * 10) + offset, 0.1f * speed * newSpeed);


                Quaternion newRotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.1f * speed * newSpeed);


            }




        }
        else
        {


            FirstArmOffset = target.transform.localPosition + target.transform.InverseTransformDirection(0.1f, distanceFromObject, -distanceFromObject) + new  Vector3(0, 0, -distanceFromObject);;
            


            transform.position = FirstArmOffset;

            //newPosition.z = 0;
            //newPosition += offset;


           // transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f * speed * newSpeed);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotationOffset), 0.1f * speed * newSpeed);

            Quaternion newRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.1f * speed * newSpeed);

        }



    }
}


