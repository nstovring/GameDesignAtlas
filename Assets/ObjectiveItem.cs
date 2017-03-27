using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : InteractableObject {

    public enum ObjectiveItemType {Key, PowerCore}

    public ObjectiveItemType type;
    public override void Interact()
    {
        base.Interact();
        

        if(type == ObjectiveItemType.Key)
            LevelManager.lm.keyscollected += 1; //rename to objectives Collected
        if (type == ObjectiveItemType.PowerCore)
            LevelManager.lm.spherescollected += 1; //rename to objectives Collected

        //Debug.Log("key collected! nmb: " + LevelManager.lm.keyscollected);
        Destroy(this.gameObject, 0.1f);
        

    }
}
