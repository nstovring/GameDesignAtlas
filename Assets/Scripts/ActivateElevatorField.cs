using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElevatorField : MonoBehaviour, ILinkable {
    Collider colider;

    public List<Renderer> LightBars;
    public Color LightEmissionColor;
    void Start()
    {
        LightEmissionColor = LightBars[0].material.GetColor("_EmissionColor");
        colider = GetComponent<Collider>();
        Deactivate();
    }
    public void Activate()
    {
        colider.enabled = true;
        foreach (var item in LightBars)
        {
            item.material.SetColor("_EmissionColor", LightEmissionColor);
        }
    }

    public void Deactivate()
    {
        colider.enabled = false;
        foreach (var item in LightBars)
        {
            item.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
