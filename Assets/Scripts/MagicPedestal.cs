using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPedestal : MonoBehaviour, IInteractable {

    public IList<ILinkable> LinkedObjects;
    public List<Transform> LinkedTansforms;
    public bool hasOrb = false;
    public void Interact()
    {
        if (!hasOrb)
        {
            Debug.Log(LevelManager.lm.spherescollected);

            foreach (var item in LinkedObjects)
            {
                if (LevelManager.lm.spherescollected > 0)
                {
                    item.Activate();
                    Debug.Log("Activate Linked Object");
                    hasOrb = true;
                    //transform.gameObject.SetActive(true);
                    if (transform.childCount > 0)
                        transform.GetChild(0).gameObject.SetActive(true);
                    //LevelManager.lm.spherescollected--;
                }

            }
            if (LevelManager.lm.spherescollected > 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                LevelManager.lm.spherescollected--;
                hasOrb = true;
            }
        }
        else
        {
            foreach (var item in LinkedObjects)
            {
                //deactivate
                item.Deactivate();
                    Debug.Log("Activate Linked Object");
                    //transform.gameObject.SetActive(true);
                    if (transform.childCount > 0)
                        transform.GetChild(0).gameObject.SetActive(false);
            }
            transform.GetChild(0).gameObject.SetActive(false);
            LevelManager.lm.spherescollected++;
            hasOrb = false;
        }
    }

    // Use this for initialization
    void Start () {
        LinkedObjects = new List<ILinkable>();
        foreach (var item in LinkedTansforms)
        {
            LinkedObjects.Add(item.GetComponent<MonoBehaviour>() as ILinkable);
        }

        if (hasOrb)
        {
            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
