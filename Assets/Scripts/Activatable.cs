using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour, ILinkable
{


    public void Activate()
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).parent = null;
        }

        gameObject.SetActive(false);

    }

    void Update()
    {

        if (transform.childCount == 0)
        {
            Activate();
        
        }


    }

}
