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

    private Rigidbody rb;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        CurrentMoveType = MovementTypes.Running;
        rb = transform.GetComponent<Rigidbody>();
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

    public CharacterController GetController()
    {
        return controller;
    }

    delegate void CurrentMovement();
    CurrentMovement MyCurrentMovement;

    public enum MovementTypes { Running, Jumping, Hanging, Climbing, Falling, Float};

    public MovementTypes CurrentMoveType;

    public void EvaluateMovement()
    {
        switch (CurrentMoveType)
        {
            case MovementTypes.Running:
                MyCurrentMovement = Run;
                break;
            case MovementTypes.Jumping:
                MyCurrentMovement = Jump;
                break;
            case MovementTypes.Hanging:
                MyCurrentMovement = Hang;
                break;
            case MovementTypes.Climbing:
                MyCurrentMovement = Climb;
                break;
            case MovementTypes.Falling:
                MyCurrentMovement = Fall;
                break;
            case MovementTypes.Float:
                MyCurrentMovement = Levitate;
                break;
        }
    }

    public void Movement()
    {
        myCharAnimator.SetDistFromGround(DistanceFromGround());

        EvaluateMovement();
        MyCurrentMovement();

        //if (isLevitating && !isHanging)
        //{
        //    Levitate();
        //}

        //if (isHanging)
        //{
        //    controller.enabled = false;
        //    myCharAnimator.myAnimator.applyRootMotion = true;
        //    controller.transform.position = Ledge.position - LedgeOffset;

        //    if (Input.GetKeyUp(KeyCode.Space))
        //    {
        //        myCharAnimator.myAnimator.SetBool("Climbing", true);
        //    }
        //}
      
        //if (DistanceFromGround()< 0.1f && !isHanging)
        //{
        //    isLevitating = false;
        //    controller.enabled = true;
        //    if (wasInAir)
        //    {
        //        myCharAnimator.SetVSpeed(1);
        //        wasInAir = false;
        //    }
        //    myCharAnimator.Jump(false);

        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        //    if (moveDirection.magnitude >= 0.5f)
        //        transform.rotation = Quaternion.LookRotation(moveDirection);

        //    moveDirection *= speed;


        //    if (Input.GetButton("Jump"))
        //    {
        //        myCharAnimator.Jump(true);
        //        moveDirection.y = jumpSpeed;
        //        myCharAnimator.SetVSpeed(0);
        //        wasInAir = true;
        //    }
        //}
        //else
        //{
        //    if (IsFalling() && !isHanging)
        //    {
        //        myCharAnimator.SetVSpeed(0.5f);
        //    }
        //}

        //if (!isHanging)
        //{
        //    gravity = Mathf.Lerp(gravity, 20, 0.1f);
        //    moveDirection.y -= gravity*Time.deltaTime;
        //    moveDirection.z = 0;
        //    controller.Move(moveDirection*Time.deltaTime);
        //    myCharAnimator.SetVelocity(controller.velocity);
        //}
    }
    bool isJumping = false;
    void Jump()
    {
        //moveDirection *= speed;
        //controller.Move(moveDirection * Time.deltaTime);
        if (isJumping == false)
        {
            myCharAnimator.Jump(true);
            isJumping = true;
        }
    }

    void EventJump()
    {
        //moveDirection*= speed/2;
        moveDirection.y = jumpSpeed/2;
        myCharAnimator.SetVSpeed(0);
        wasInAir = true;
        if (rb.velocity.sqrMagnitude < maxSpeed)
            rb.AddForce(moveDirection,ForceMode.Impulse);
        //controller.Move(moveDirection * Time.deltaTime);

        myCharAnimator.Jump(false);
        isJumping = false;
        CurrentMoveType = MovementTypes.Falling;

      

    }
    void Fall()
    {
        //moveDirection.y -= gravity * Time.deltaTime;
        //moveDirection.z = 0;
        //controller.Move(moveDirection * Time.deltaTime);
        myCharAnimator.SetDistFromGround(DistanceFromGround());
        if (DistanceFromGround() < 0.25f || controller.isGrounded)
        {
            CurrentMoveType = MovementTypes.Running;
        }
    }

    void Run()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveDirection.magnitude >= 0.5f)
            transform.rotation = Quaternion.LookRotation(moveDirection);

        moveDirection *= speed;

        gravity = 40;//Mathf.Lerp(gravity, 20, 0.5f);
        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection.z = 0;
        if(rb.velocity.sqrMagnitude < maxSpeed)
        rb.AddForce(moveDirection * speed, ForceMode.Acceleration);
        //controller.Move(moveDirection * Time.deltaTime);
        myCharAnimator.SetVelocity((rb.velocity/speed)/1.15f);

        if (Input.GetButton("Jump"))
        {
            CurrentMoveType = MovementTypes.Jumping;
        }
    }

    public float maxSpeed = 10;
    void Hang()
    {

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        
        myCharAnimator.myAnimator.applyRootMotion = true;
        myCharAnimator.HangOnLedge(true);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CurrentMoveType = MovementTypes.Climbing;
        }
    }

    void Climb()
    {
        myCharAnimator.Climb(true);
    }

    public void FinishedClimbing()
    {
        Debug.Log("Finished Climbing");
        myCharAnimator.Climb(false);
        rb.isKinematic = false;

        myCharAnimator.myAnimator.applyRootMotion = false;
        CurrentMoveType = MovementTypes.Running;
    }

    void Levitate()
    {
        gravity = 0;
        myCharAnimator.SetDistFromGround(DistanceFromGround());
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed;

        //if (moveDirection.magnitude >= 0.5f)
        transform.rotation = Quaternion.LookRotation(moveDirection);

        moveDirection.z = 0;

        //moveDirection.y += Time.deltaTime * 50;
        if (rb.velocity.sqrMagnitude < maxSpeed)
            rb.AddForce(moveDirection * 200 + transform.up*10, ForceMode.Acceleration);
        //controller.Move(moveDirection);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.position.y > transform.position.y && other.transform.tag == "Ledge")
        {
            isHanging = true;
            Ledge = other.transform;
            CurrentMoveType = MovementTypes.Hanging;
        }
        if (other.transform.tag == "Interactable")
        {
            Debug.Log("Interactable");

            Component[] otherComponents =other.GetComponents(typeof(Component));

            foreach (var component in otherComponents)
            {
                MonoBehaviour monoComponent = component as MonoBehaviour;
                if(monoComponent is IInteractable)
                {
                    Debug.Log("has Interface");

                    IInteractable iObject = (IInteractable)monoComponent;
                    iObject.Interact();
                    break;
                }
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "ElevatorField")
        {
            Debug.Log("Exiting levitation field");
            isLevitating = false;
            CurrentMoveType = MovementTypes.Falling;
        }
    }
    bool isLevitating = false;

  
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "ElevatorField")
        {
            Debug.Log("Levitate!");
            CurrentMoveType = MovementTypes.Float;
            isLevitating = true;
        }

        if (other.transform.tag == "PlayerInteractable")
        {

            if (Input.GetKeyUp(KeyCode.Q))
            {
                Debug.Log("Interacting");
                MonoBehaviour hitBehaviour = other.GetComponent<MonoBehaviour>();
                if (hitBehaviour is IInteractable)
                {
                    IInteractable iObject = (IInteractable)hitBehaviour;
                    iObject.Interact();
                }
            }
        }
    }


}
