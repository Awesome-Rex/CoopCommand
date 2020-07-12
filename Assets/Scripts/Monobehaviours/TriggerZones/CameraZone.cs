using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public CamState state;
    public bool showAllFloors = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameplayControl.I.inControl.gameObject)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameplayCamera.I.state = state;
                GameplayCamera.I.showAllFloors = showAllFloors;
            } else if (GameplayCamera.I.state == CamState.Ship)
            {
                GameplayCamera.I.state = CamState.Ground;
                GameplayCamera.I.showAllFloors = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameplayControl.I.inControl.gameObject)
        {
            GameplayCamera.I.state = CamState.Ground;
            GameplayCamera.I.showAllFloors = false;
        }
    }
}
