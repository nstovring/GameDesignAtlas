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
        if (other.tag == "Key")
        {
            Destroy(other.gameObject);
            LevelManager.lm.keyscollected += 1;
            Debug.Log("key collected! nmb: " + LevelManager.lm.keyscollected);

        }

        if (other.tag == "Sphere")
        {
            Destroy(other.gameObject);
            LevelManager.lm.spherescollected += 1;
            Debug.Log("sphere collected! nmb: " + LevelManager.lm.spherescollected);

        }

        if (other.tag == "LevelEnd")
        {
            if (LevelManager.lm.keyscollected == 1)
            {
                Debug.Log("congratulations, you won!");
            }
        }

        if (other.tag == "Interactable")
        {
            if (LevelManager.lm.spherescollected == 1)
            {
               Debug.Log("activate ");
            }
        }

        if (other.tag == "Boundarycheck")
        {
            Debug.Log("restart game");
            LevelManager.lm.restartLevel();
            
        }

    }
}
