﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsHanging", false);
        animator.SetBool("Climbing", true);
        //animator.applyRootMotion = true;
        if(animator.transform.parent == null)
        {
            animator.transform.parent = animator.transform.GetComponent<CharacterMotorNew>().colidingObject;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ApplyBuiltinRootMotion();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Climbing = false;
        CharacterMotorNew myMotor = animator.transform.GetComponent<CharacterMotorNew>();
        myMotor.controller.enabled = true;
        animator.applyRootMotion = false;
        animator.SetBool("IsHanging", false);
        animator.SetBool("Climbing", false);
        myMotor.canControl = true;
        animator.transform.parent = null;

        //myMotor.FinishedClimbing();
        // animator.applyRootMotion = false;
    }

    //OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = new Vector3(animator.rootPosition.x, animator.rootPosition.y,0);
    }

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
