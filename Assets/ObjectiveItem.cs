using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : InteractableObject {

    public enum ObjectiveItemType {Key, PowerCore, Artifact}

    public ObjectiveItemType type;
    public override void Interact()
    {
        base.Interact();


        LevelManager.lm.SetObjectivesGathered(type);

        //if (type == ObjectiveItemType.Key)
        //    LevelManager.lm.keyscollected += 1;
        //if (type == ObjectiveItemType.PowerCore)
        //    LevelManager.lm.spherescollected += 1; 
        //if (type == ObjectiveItemType.Artifact)
        //    LevelManager.lm.timeShiftingClass.CanPlayerTimeShift = true; 


        //Debug.Log("key collected! nmb: " + LevelManager.lm.keyscollected);
        Destroy(this.gameObject, 0.1f);
        

    }
}
