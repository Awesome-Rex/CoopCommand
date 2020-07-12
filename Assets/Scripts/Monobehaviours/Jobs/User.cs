using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviourPRO
{
    public Usable used;

    public void Use (Usable usable)
    {
        usable.used = true;

        used = usable;
    }

    public void StopUsing ()
    {
        used.used = false;

        used = null;
    }
}
