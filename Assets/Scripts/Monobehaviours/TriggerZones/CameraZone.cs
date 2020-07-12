using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public CamState state;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameplayControl.I.inControl.gameObject)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameplayCamera.I.state = state;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameplayControl.I.inControl.gameObject)
        {
            GameplayCamera.I.state = CamState.Ground;
        }
    }
}
