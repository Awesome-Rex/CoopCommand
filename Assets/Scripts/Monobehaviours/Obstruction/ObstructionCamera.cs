using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionCamera : MonoBehaviour
{
    public Transform subject;

    public bool BetweenSubject (Vector3 position)
    {
        if (subject != null)
        {
            if (transform.InverseTransformPoint(position).z > 0f &&
                transform.InverseTransformPoint(position).z < transform.InverseTransformPoint(subject.position).z)
            {
                return true;
            } else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
