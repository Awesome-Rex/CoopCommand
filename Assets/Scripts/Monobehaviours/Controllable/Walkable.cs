using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controllable))]
public class Walkable : MonoBehaviourPRO
{
    public float speed = 2.5f;
    public float velocityLimit = 2f;

    private Vector3 direction;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Controllable>().inControl)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (direction != Vector3.zero && GetComponent<Rigidbody>().velocity.magnitude < velocityLimit)
            {
                //adds global position
                GetComponent<Rigidbody>().position += (
                    GameplayControl.I.ship.transform.InverseTransformPoint(GameplayControl.I.ship.transform.position + direction).normalized
                    * speed * Time.deltaTime);
            }
        }
    }
}
