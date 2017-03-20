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
    bool isHanging = false;

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
            if (Input.GetButton("Jump"))
            {
                isHanging = false;
                myCharAnimator.HangOnLedge(false);
            }
        }
        if (DistanceFromGround()< 0.1f && !isHanging)
        {
            if (wasInAir)
            {
                myCharAnimator.SetVSpeed(1);
                Debug.Log("Landing");
                
                wasInAir = false;
                //StartCoroutine(StopJumpingDelayed(0.01f));
            }
            myCharAnimator.Jump(false);
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (moveDirection.magnitude >= 0.1)
                transform.rotation = Quaternion.LookRotation(moveDirection);
            // moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                myCharAnimator.Jump(true);
                moveDirection.y = jumpSpeed;
                myCharAnimator.SetVSpeed(0);
                Debug.Log("Jumping");
                wasInAir = true;
            }
        }
        else
        {
            //myCharAnimator.Jump(true);
            if (IsFalling() && !isHanging)
            {
                myCharAnimator.SetVSpeed(0.5f);
                Debug.Log("Idle Air");
            }
            //myCharAnimator.SetVSpeed();
        }

        //myCharAnimator.SetJumpSpeed(controller.velocity.normalized.y);
        if (!isHanging)
        {
            moveDirection.y -= gravity*Time.deltaTime;
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
        }
        Destroy(other.gameObject);
    }

}
