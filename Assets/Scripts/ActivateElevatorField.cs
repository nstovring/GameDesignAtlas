using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElevatorField : MonoBehaviour, ILinkable {
    Collider colider;
    void Start()
    {
        colider = GetComponent<Collider>();
        colider.enabled = false;
    }
    public void Activate()
    {
        colider.enabled = true;
    }

    public void Deactivate()
    {
        colider.enabled = false;
    }
}
