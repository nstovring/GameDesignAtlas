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
	
	// Update is called once per frame
	void Update () {
		
	}
}
