using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviourPRO
{
    public List<MeshRenderer> toHide;

    public bool hidden = false;

    public void Hide (bool hidden)
    {
        this.hidden = hidden;

        foreach (MeshRenderer i in toHide) { 
            if (hidden)
            {
                i.enabled = false;
            } else
            {
                i.enabled = true;
            }
        }
    }

    [ContextMenu("Get Meshes")]
    public void GetMeshes()
    {
        toHide = new List<MeshRenderer>();

        foreach (MeshRenderer i in GetComponentsInChildren<MeshRenderer>())
        {
            toHide.Add(i);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameplayControl.I.inControl)
        {
            GameplayControl.I.TransitionFloor(this);
        }
    }
}
