using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMotor : MonoBehaviour {
    public List<Transform> PatrolPoints;

    public Animator myAnimator;
	// Use this for initialization
	void Start () {
        myAnimator.GetComponent<Animator>();
	}
    bool alert = false;
    bool patrolling = false;
    // Update is called once per frame

    Transform target;

    float movementMagnitude;

    void Update () {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + transform.up, transform.forward);
        Debug.DrawRay(transform.position + transform.up, transform.forward * 5, Color.red);

        Collider[] collider = Physics.OverlapSphere(transform.position + transform.forward * 5, 5f);

        foreach (var item in collider)
        {
            if(item.transform.tag == "Player")
            {
                alert = true;
                target = item.transform;
                break;
            }
        }

        if (alert)
        {
            Vector3 moveDirection = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(moveDirection);
            movementMagnitude = Mathf.Lerp(movementMagnitude, moveDirection.magnitude, 0.1f);
            myAnimator.SetFloat("HSpeed", movementMagnitude);
        }
        else
        {
            movementMagnitude = Mathf.Lerp(movementMagnitude, 0, 0.1f);
            myAnimator.SetFloat("HSpeed", movementMagnitude);

            //Vector3 moveDirection = transform.position - target.position;
            //transform.rotation = Quaternion.LookRotation(moveDirection);
            //myAnimator.SetFloat("HSpeed", moveDirection.magnitude);
        }

        if (alert && Vector3.Distance(transform.position,target.position) > 10)
        {
            alert = false;
        }
	}
}
