using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Controllable))]
public class Aimable : MonoBehaviour
{
    public Vector3 mousePosition;
    
    public Vector3 direction
    {
        get
        {
            Vector3 thisPos = Camera.main.WorldToScreenPoint(transform.position);
            thisPos = new Vector3(thisPos.x, thisPos.y, 0f);

            return (Camera.main.ScreenToWorldPoint(/*Mouse.current.position.ReadValue()*/mousePosition)) - thisPos;
        }
    }

    private void Awake()
    {
        _ETERNAL.I.controls.Gameplay.Mouse.performed += (ctx) => mousePosition = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (GetComponent<Controllable>().inControl)
        {
            transform.forward = direction;
        }
    }

    private void Destroy()
    {
        _ETERNAL.I.controls.Gameplay.Mouse.performed -= (ctx) => mousePosition = ctx.ReadValue<Vector2>();
    }

    private void OnDrawGizmos()
    {
        Debug.Log(_ETERNAL.I.controls.Gameplay.Mouse.ReadValue<Vector2>());
        //Debug.Log(mousePosition);

        Gizmos.DrawLine(transform.position, transform.position + (direction.normalized * 2f));

        Gizmos.DrawSphere(transform.position + (direction.normalized * 2f), 1f);
    }
}
