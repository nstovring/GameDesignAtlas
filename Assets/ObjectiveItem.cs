using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : InteractableObject {
    public override void Interact()
    {
        base.Interact();
        


        LevelManager.lm.keyscollected += 1; //rename to objectives Collected
        Debug.Log("key collected! nmb: " + LevelManager.lm.keyscollected);
        Destroy(this.gameObject, 0.1f);
        

    }
}
