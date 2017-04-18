using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatObject : InteractableObject {
    public override void Interact()
    {
        base.Interact();

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelManager>().gameStarted = false;
        
        LevelManager.lm.restartLevel();


    }
}
