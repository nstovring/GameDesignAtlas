using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatObject : InteractableObject {
    public override void Interact()
    {
        base.Interact();


        LevelManager.lm.restartLevel();


    }
}
