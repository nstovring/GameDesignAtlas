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
        Debug.DrawRay(transform.position + transform.up, transform.forward * 5, Color.red);

        Collider[] collider = Physics.OverlapSphere(transform.position, 10f);
        RaycastHit hit;

        Ray ray = new Ray(transform.position + transform.up, transform.forward);
        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.transform.tag != "Player")
            {
                alert = false;
                target = null;
            }
        }   


        foreach (var item in collider)
        {
            if(item.transform.tag == "Player")
            {
                ray = new Ray(transform.position + transform.up, (item.transform.position - transform.position));
                Debug.DrawRay(transform.position + transform.up, (item.transform.position - transform.position) * 10);
                if (Physics.Raycast(ray, out hit, 10))
                {
                    if(hit.transform.tag == "Player")
                    {
                        alert = true;
                        target = item.transform;
                        break;
                    }
                }
            }
        }

        if (alert && myAnimator.enabled == true)
        {
            Vector3 moveDirection = target.position - transform.position;
            transform.rotation = Quaternion.Euler(0,Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(moveDirection),0.2f).eulerAngles.y,0);

            movementMagnitude = Mathf.Lerp(movementMagnitude, 1, 0.01f);
            myAnimator.SetFloat("HSpeed", movementMagnitude);
        }
        else if(PatrolPoints.Count > 0 && !alert)
        {
            Vector3 moveDirection = PatrolPoints[patrolInt].position - transform.position;
            transform.rotation = Quaternion.Euler(0, Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.2f).eulerAngles.y, 0);

            movementMagnitude = Mathf.Lerp(movementMagnitude, 0.3f, 0.1f);
            myAnimator.SetFloat("HSpeed", movementMagnitude);
            if(Vector3.Distance(transform.position, PatrolPoints[patrolInt].position) < 1f)
            {
                patrolInt++;
                patrolInt = patrolInt % PatrolPoints.Count;
            }
        }
        else
        {
            movementMagnitude = Mathf.Lerp(movementMagnitude, 0, 0.1f);
            myAnimator.SetFloat("HSpeed", movementMagnitude);
        }

        if (alert && Vector3.Distance(transform.position,target.position) > 10)
        {
            alert = false;
        }
	}

    int patrolInt = 0;

    void ApplyGravity()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 7);
    }
}
