using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    public Transform playerCharacter;

    public Vector3 offset = Vector3.zero;
	// Use this for initialization
	void Start () {
        offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();
	}

    void FollowPlayer()
    {
        Vector3 newPosition = playerCharacter.position;
        newPosition.z = 0;

        newPosition += offset;

        transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
    }
}
