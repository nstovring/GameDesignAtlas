using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPedestal : MonoBehaviour, IInteractable {

    public IList<ILinkable> LinkedObjects;
    public List<Transform> LinkedTansforms;
    public void Interact()
    {
        foreach (var item in LinkedObjects)
        {
            if (LevelManager.lm.spherescollected == 1)
            {
                item.Activate();

                transform.GetChild(0).gameObject.SetActive(true);
            }
            
        }
    }

    // Use this for initialization
    void Start () {
        LinkedObjects = new List<ILinkable>();
        foreach (var item in LinkedTansforms)
        {
            LinkedObjects.Add(item.GetComponent<MonoBehaviour>() as ILinkable);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
