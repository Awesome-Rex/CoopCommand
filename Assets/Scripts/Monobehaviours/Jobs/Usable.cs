using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Repairable))]
public class Usable : MonoBehaviourPRO
{
    public bool used;

    public float radius = 1f;

    public bool canUse (Transform point)
    {
        return Vector3.Distance(point.position, transform.position) <= radius;
    }
    
    void Awake()
    {
        GetComponent<Health>().EDead += () => {
            GetComponent<Repairable>().repairProgress = 0f;
            GetComponent<Repairable>().needsRepair = true;
        };
    }
}
