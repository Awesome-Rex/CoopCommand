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
                Input.mousePosition - 
                GameplayCamera.I.camera.ScreenToScreenPoint(GameplayCamera.I.camera.WorldToScreenPoint(transform.position), Camera.main);
        }
    }
    
    void FixedUpdate()
    {
        Debug.Log(GameplayCamera.I.camera.ScaleFromTexture(GameplayCamera.I.camera.WorldToScreenPoint(transform.position)).ToString() + " -- " +
             Camera.main.WorldToScreenPoint(transform.position).ToString());

        if (GetComponent<Controllable>().inControl)
        {
            transform.forward = new Vector3(direction.x, 0f, direction.y);
        }
    }
}
