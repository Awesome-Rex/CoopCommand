using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controllable))]
public class Walkable : MonoBehaviourPRO
{
    public float speed = 2.5f;
    public float velocityLimit = 2f;

    private Vector3 direction;
    private Vector3 setDirection;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Controllable>().inControl)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

            if (direction != Vector3.zero && GetComponent<Rigidbody>().velocity.magnitude < velocityLimit)
            {
                //flat direction
                setDirection = (Quaternion.LookRotation(direction.normalized) * Quaternion.Euler(0f, Camera.main.transform.eulerAngles.z, 0f)) * Vector3.forward * direction.magnitude;

                GetComponent<Rigidbody>().position += (
                    GameplayControl.I.ship.transform.InverseTransformPoint(GameplayControl.I.ship.transform.position + setDirection).normalized
                    * speed * Time.deltaTime);
            }
        }
    }
}
