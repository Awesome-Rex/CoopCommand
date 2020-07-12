using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviourPRO
{
    public bool inControl
    {
        get
        {
            return GameplayControl.I.inControl == this;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
