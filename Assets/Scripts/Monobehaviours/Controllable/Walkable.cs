using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controllable))]
public class Walkable : MonoBehaviourPRO
{
    public float speed = 2.5f;

    private Vector3 direction;
    private Vector3 setDirection;

    protected void Awake()
    {
        _ETERNAL.I.controls.Gameplay.Movement.performed += (ctx) => direction = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Controllable>().inControl)
        {
            if (direction != Vector3.zero)
            {
                setDirection = (Quaternion.LookRotation(direction.normalized) * Quaternion.Euler(0f, Camera.main.transform.eulerAngles.z, 0f)) * Vector3.forward * direction.magnitude;

                GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (
                    GameplayControl.I.ship.transform.InverseTransformPoint(GameplayControl.I.ship.transform.position + direction).normalized
                    * speed * Time.deltaTime));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, GetComponent<Rigidbody>().position + (
                    GameplayControl.I.ship.transform.InverseTransformPoint(GameplayControl.I.ship.transform.position + direction).normalized
                    * speed * Time.deltaTime));
    }

    protected void OnDestroy()
    {
        _ETERNAL.I.controls.Gameplay.Movement.performed -= (ctx) => direction = ctx.ReadValue<Vector2>();
    }
}
