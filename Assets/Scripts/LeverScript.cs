using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour, IInteractable {
    public ElevatorScript linkedElevator;
    public GateWay linkedScript;
    public bool activated = false;

    public Animator leverAnimator;
    public void Interact()
    {
        Debug.Log("Interacing with lever");
        activated = !activated;
        Animate(activated);

        if (linkedElevator != null)
        {
            linkedElevator.StartElevator = !linkedElevator.StartElevator;
            linkedElevator.SetState(linkedElevator.StartElevator);
            //activated = linkedElevator.StartElevator;
        }
        else if (linkedScript != null)
        {
            if (!linkedScript.open && !activated)
            {
                //Animate(activated);
                StartCoroutine(RaiseAndShutGate());
            }
        }
    }

    void Animate(bool state)
    {
        leverAnimator.SetBool("On", state);
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
