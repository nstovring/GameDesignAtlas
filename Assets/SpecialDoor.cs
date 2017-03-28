using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDoor : MonoBehaviour, IInteractable {

    public int currentUnlockedLocks = 0;

    public void Interact()
    {
        currentUnlockedLocks = LevelManager.lm.keyscollected;
        OpenLock();
    }

    public Animator animator;

    void OpenLock()
    {
        animator.SetInteger("OpenedLocks", currentUnlockedLocks);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
