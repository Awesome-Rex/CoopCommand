using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorZone : MonoBehaviourPRO
{
    public new string name;

    public List<MeshRenderer> toHide;
    //public List<MeshRenderer> toHideCurrent;

    public bool hidden
    {
        get
        {
            return _hidden;
        }
        set
        {
            _hidden = value;

            Debug.Log(value);

            foreach (MeshRenderer i in toHide)
            {
                if (value)
                {
                    i.enabled = false;
                }
                else
                {
                    i.enabled = true;
                }
            }
        }
    }

    private bool _hidden = false;

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
        if (other.gameObject == GameplayControl.I.inControl.gameObject)
        {
            //GameplayControl.I.TransitionFloor(this);
        }
    }

    private void Update()
    {
        
    }
}
