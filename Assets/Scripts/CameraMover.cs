using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    public Transform playerCharacter;

    public Vector3 offset = new Vector3(0,4.14f,-10);
    public Vector3 rotationOffset = Vector3.zero;
    public bool SplineMovement = false;
    public float HSpeed;
	// Use this for initialization
	void Start () {
        //offset = transform.position;
        transform.LookAt(playerCharacter);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        
       
        FollowPlayer();
	}

    public float speed = 1;
    public float newSpeed = 1;
    public float sign = 0f;
    void FollowPlayer()
    {
        Vector3 newPosition = playerCharacter.position;
        HSpeed = Input.GetAxis("Horizontal");

        if (SplineMovement)
        {
            newPosition += offset;
           // transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f * speed * newSpeed);
           

            Vector3 CameraOrthogonalPlayer = Vector3.Cross(playerCharacter.transform.forward, playerCharacter.transform.up);
            Debug.DrawRay(transform.position, CameraOrthogonalPlayer * 10, Color.red);

//             sign = Mathf.Sign(Vector3.Cross(CameraOrthogonalPlayer, transform.forward).y);

            Debug.DrawRay(transform.position, Vector3.Cross(playerCharacter.transform.forward, transform.forward) * 10, Color.green);

            if(HSpeed > 0.9f || HSpeed < -0.9f)
            {
              transform.position =  (playerCharacter.transform.position - (HSpeed * CameraOrthogonalPlayer)*10) + offset;
                transform.position = Vector3.Lerp(transform.position, (playerCharacter.transform.position - (HSpeed * CameraOrthogonalPlayer) * 10) + offset, 0.1f * speed * newSpeed);

               // transform.LookAt(playerCharacter);

                Quaternion newRotation = Quaternion.LookRotation( playerCharacter.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.1f * speed * newSpeed);

                //lerp to look and position
            }
            



           

            //  if(sign > 0)
            //  transform.rotation = Quaternion.LookRotation( CameraOrthogonalPlayer);


        }
        else
        {




            //speed = Mathf.Lerp(speed, newSpeed, 0.1f);
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(newPosition);
            
            newPosition.z = 0;
            //Vector3 targetDir = newPosition - transform.position;

            newPosition += offset;

            transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f * speed * newSpeed);
            transform.LookAt(playerCharacter);
            //transform.LookAt(playerCharacter);

        }



    }
}
