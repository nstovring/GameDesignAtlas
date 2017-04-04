using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clampText : MonoBehaviour
{

    public Text textLabel;
    public Vector3 textPos;
    
    // Use this for initialization
	void Start ()
	{

	   

	}

    void Update()
    {
          textPos = Camera.main.ScreenToViewportPoint(this.transform.position);

	    textLabel.transform.position = textPos;
    }
	
	
    


}
