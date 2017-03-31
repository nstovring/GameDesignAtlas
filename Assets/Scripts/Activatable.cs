using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour, ILinkable
{


    public void Activate()
    {

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
