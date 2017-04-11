using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, IInteractable {
    public ElevatorScript linkedElevator;
    public GateWay linkedScript;
    public bool activated = false;
    public void Interact()
    {
        Debug.Log("Interacing with lever");
        if(linkedElevator != null)
        {
            linkedElevator.StartElevator = !linkedElevator.StartElevator;
        }
        else if (linkedScript != null)
        {
            if (!linkedScript.open && !activated)
            {
                StartCoroutine(RaiseAndShutGate());
            }
        }
    }
    IEnumerator RaiseAndShutGate()
    {
        activated = true;
        linkedScript.Activate();
        yield return new WaitForSeconds(5);
        linkedScript.Deactivate();
        activated = false;
    }
}
