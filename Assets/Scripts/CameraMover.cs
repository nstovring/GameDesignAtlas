﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    public Transform playerCharacter;

    public Vector3 offset = new Vector3(0,4.14f,-10);
    public Vector3 rotationOffset = Vector3.zero;
	// Use this for initialization
	void Start () {
        //offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        FollowPlayer();
	}

    public float speed = 1;
    public float newSpeed = 1;
    void FollowPlayer()
    {
        Vector3 newPosition = playerCharacter.position;
        //speed = Mathf.Lerp(speed, newSpeed, 0.1f);
        Vector3 playerScreenPos =  Camera.main.WorldToScreenPoint(newPosition);
        //Vector3 StoredPos 
        //if (playerScreenPos.x -5 > (Screen.width / 2) + ScreenXLimit || playerScreenPos.x +5< (Screen.width/2) - ScreenXLimit)
        //{
            newPosition.z = 0;
            newPosition += offset;
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f * speed* newSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotationOffset), 0.1f * speed * newSpeed);
        //}
       
    }
}
