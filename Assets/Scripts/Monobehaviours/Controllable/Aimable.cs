using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

[RequireComponent(typeof(Controllable))]
public class Aimable : MonoBehaviour
{
    public Vector3 direction
    {
        get
        {
            return 
                (Input.mousePosition) - 
                GameplayCamera.I.camera.ScaleFromTexture(GameplayCamera.I.camera.WorldToScreenPoint(transform.position));
        }
    }
    
    void FixedUpdate()
    {
        if (GetComponent<Controllable>().inControl)
        {
            transform.forward = new Vector3(direction.x, 0f, direction.y);
        }
    }
}
