using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

using TransformTools;

public class ForTESTING : MonoBehaviourPRO
{
    private int counter;

    private void Awake()
    {
        counter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (//!GetComponent<ParentConstraint>().constraintActive == true
            counter % 4 == 0
            ) {
            GetComponent<ParentConstraint>().constraintActive = false;
        } else if ((counter - 2) % 4 == 0)
        {
            GetComponent<ParentConstraint>().translationAtRest = GetComponent<Rigidbody>().position;
            //GetComponent<ParentConstraint>().GetSource(0).sourceTransform.InverseTransformPoint(transform.position);

            GetComponent<ParentConstraint>().rotationAtRest = GetComponent<Rigidbody>().rotation.eulerAngles;
            //Linking.InverseTransformEuler(transform.rotation, GetComponent<ParentConstraint>().GetSource(0).sourceTransform.rotation).eulerAngles;

            GetComponent<ParentConstraint>().constraintActive = true;
        }

        //counter = !counter;
        counter += 1;
    }
}
