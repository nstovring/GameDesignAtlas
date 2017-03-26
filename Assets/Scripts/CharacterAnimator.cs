using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator myAnimator;
    public CharacterMotor myMotor;
	// Use this for initialization
	void Start ()
	{
	    //myAnimator.applyRootMotion = true;
	}

    private float HSpeed;
    public void SetVelocity(Vector3 CharVelocity)
    {
        //HSpeed = Mathf.Lerp(HSpeed,CharVelocity.normalized.x * Mathf.Sign(CharVelocity.x),0.1f);
        myAnimator.SetFloat("HSpeed", CharVelocity.sqrMagnitude);
    }

    private float VSpeed;
    public void SetVSpeed(float speed)
    {
        //VSpeed = Mathf.Lerp(VSpeed, speed, 0.5f);

        myAnimator.SetFloat("VSpeed", speed);
    }

    public void SetDistFromGround(float dist)
    {
        myAnimator.SetFloat("DistFromGround", dist);
    }
    public void Jump(bool jumping)
    {
        myAnimator.SetBool("Jumping", jumping);
    }

    public void HangOnLedge(bool hanging)
    {
        myAnimator.SetBool("IsHanging", hanging);
    }

    public void Climb(bool climbing)
    {
        HangOnLedge(false);
        myAnimator.SetBool("Climbing", climbing);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
