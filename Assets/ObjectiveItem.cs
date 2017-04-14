using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : InteractableObject {

    public enum ObjectiveItemType {Key, PowerCore, Artifact}

    public ObjectiveItemType type;
    public GameObject ParticlePickupEffectTransform;
    void Start()
    {
        if(ParticlePickupEffectTransform)
        ParticlePickupEffectTransform.active = false;
    }
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
        MagicPedestal tempPedestal = GetComponent<MagicPedestal>();
        if (tempPedestal!= null)
        {
            tempPedestal.Interact();
        }
        float destroyTime = 0.5f;
        if(type == ObjectiveItemType.PowerCore)
        {
            if (ParticlePickupEffectTransform)
            {
                ParticlePickupEffectTransform.active = true;
                destroyTime = 1;
            }

        }

        //Debug.Log("key collected! nmb: " + LevelManager.lm.keyscollected);
        Destroy(this.gameObject, destroyTime);
        

    }
}
