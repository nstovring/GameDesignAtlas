using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereItem : InteractableObject {


    public override void Interact()
    {
        base.Interact();



        LevelManager.lm.spherescollected += 1; //rename to objectives Collected
        Debug.Log("sphere collected! nmb: " + LevelManager.lm.spherescollected);
        Destroy(this.gameObject, 0.1f);


    }
}
