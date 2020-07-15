using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

[RequireComponent(typeof(ScriptReference))]
public abstract class MonoBehaviourPRO : MonoBehaviour
{
    public ScriptReference SR
    {
        get
        {
            if (_SR == null)
            {
                _SR = base.GetComponent<ScriptReference>();
            }

            return _SR;
        }
    }
    private ScriptReference _SR;

    public new T GetComponent<T> ()
    {
        return SR.Get<T>();
    }

    public T ResourcesLoad<T>(string path) where T : Object
    {
        return _ETERNAL.I.resourceReference.Load<T>(path);
    }
}
