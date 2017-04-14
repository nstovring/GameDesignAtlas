using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITriggerScript : MonoBehaviour, IInteractable {

    public String text; // Text you want on GUI element....duh

    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(5);

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Interact()
    {

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text= text;
        
        StartCoroutine(Fade());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
