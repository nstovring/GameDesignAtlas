using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItempPickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectible")
        {
            Destroy(other.gameObject);
        }
        LevelManager.lm.keyscollected += 1;
        Debug.Log("key collected! nmb: " + LevelManager.lm.keyscollected );
    }
}
