using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public CharacterAnimator myCharAnimator;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        //StartCoroutine(AnimationQueues());
    }


    private bool IsFalling()
    {
        if ((int) Mathf.Sign(controller.velocity.y*2) == -1)
        {
            return true;
        }
        return false;
    }

    public bool wasInAir = false;
    void Update()
    {
        Movement();
    }
    float DistanceFromGround()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit))
        {
            return Vector3.Distance(transform.position, hit.point);
        }
        return 0;
    }
    public bool isHanging = false;

    Transform Ledge;
    public Vector3 LedgeOffset;
    public void Movement()
    {
        //if (DistanceFromGround() < 0.4 && IsFalling())
        //{
        //    myCharAnimator.SetVSpeed(1);
        //    Debug.Log("Landing");
        //}
        myCharAnimator.SetDistFromGround(DistanceFromGround());
        if (isHanging)
        {
            controller.enabled = false;
            myCharAnimator.myAnimator.applyRootMotion = true;
            //controller.transform.position = Ledge.position - LedgeOffset;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                myCharAnimator.myAnimator.SetBool("Climbing", true);
            }
        }
      
        if (DistanceFromGround()< 0.1f && !isHanging)
        {
            controller.enabled = true;
            if (wasInAir)
            {
                myCharAnimator.SetVSpeed(1);
                wasInAir = false;
            }
            myCharAnimator.Jump(false);
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (moveDirection.magnitude >= 0.1)
                transform.rotation = Quaternion.LookRotation(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                myCharAnimator.Jump(true);
                moveDirection.y = jumpSpeed;
                myCharAnimator.SetVSpeed(0);
                wasInAir = true;
            }
        }
        else
        {
            if (IsFalling() && !isHanging)
            {
                myCharAnimator.SetVSpeed(0.5f);
            }
        }

        if (!isHanging)
        {
            moveDirection.y -= gravity*Time.deltaTime;
            Debug.Log("Falling gravity");
            controller.Move(moveDirection*Time.deltaTime);
            myCharAnimator.SetVelocity(controller.velocity);
        }
    }

    public IEnumerator StopJumpingDelayed(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        myCharAnimator.Jump(false);
        wasInAir = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.tag == "Ledge")
        {
            myCharAnimator.HangOnLedge(true);
            isHanging = true;
            Ledge = other.transform;
        }
    }

}
