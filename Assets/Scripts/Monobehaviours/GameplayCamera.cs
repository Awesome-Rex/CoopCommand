using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamState { Ship, ShipBack, Ground }

public class GameplayCamera : MonoBehaviour
{
    public static GameplayCamera I;
    [HideInInspector]
    public Camera camera;

    public float mousePadding = 0.25f;

    [Space]
    public float normalFOV = 60f;

    public float speed = 100f;
    public float rotateSpeed = 500f;
    public float fieldOfViewSpeed = 5f;

    [Space]
    //ground
    public float groundDistance = 5f;
    public float maxAimOffset = 0.4f;
    public float maxAimOffsetWorld = 5f;

    [Space]
    //ship
    public float shipDistance = 15f;

    public float maxShipDegreees = 100f;
    public float shipOffsetDegrees = 50f;

    [Space]
    public float shipBackDistance = 20f;
    public float shipBackOffsetDegrees = 20f;
    public float shipBackOffsetDegreePos = 50f;
    public float shipBackOffsetFOV = 100f;

    //dynamic
    public CamState state = CamState.Ground;
    public bool showAllFloors = false;

    //other
    private float percentOff;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float targetFOV;

    private void Awake()
    {
        I = this;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        percentOff = (Camera.main.ScreenToViewportPoint(Input.mousePosition) - (Vector3.right / 2f)).x * 2f;
        Mathf.Clamp(percentOff, -1f, 1f);
        //adds padding so cursor doesn't need to be at end off screen
        percentOff = Mathf.Clamp(-percentOff * (1f + mousePadding), -1f, 1f);

        //ground
        if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x <= 1f && Camera.main.ScreenToViewportPoint(Input.mousePosition).x >= 0f &&
            Camera.main.ScreenToViewportPoint(Input.mousePosition).y <= 1f && Camera.main.ScreenToViewportPoint(Input.mousePosition).y >= 0f
            ) {
            if (state == CamState.Ground) {
                targetPosition = GameplayControl.I.inControl.transform.position + (GameplayControl.I.ship.up * groundDistance);
                //adds aim offset
                Vector3 offset = new Vector3(
                    (Input.mousePosition.x - (Camera.main.pixelWidth / 2f)) / Camera.main.pixelHeight,
                    0f,
                    (Input.mousePosition.y - (Camera.main.pixelHeight / 2f)) / Camera.main.pixelHeight);
                offset = Vector3.ClampMagnitude(offset, maxAimOffset) * (maxAimOffsetWorld / maxAimOffset);

                targetPosition += offset;

                targetRotation = Quaternion.LookRotation(-GameplayControl.I.ship.transform.up);

                targetFOV = normalFOV;
            } else if (state == CamState.Ship) //side
            {
                targetPosition = GameplayControl.I.ship.transform.position +
                    (Quaternion.Euler(new Vector3(0f, 0f, maxShipDegreees * percentOff)) * 
                    Vector3.up * shipDistance);

                if (Input.GetMouseButton(0)) {
                    targetRotation = Quaternion.LookRotation(GameplayControl.I.ship.transform.position - transform.position) * Quaternion.Euler(Vector3.right * shipOffsetDegrees);
                } else
                {
                    targetRotation = Quaternion.LookRotation(GameplayControl.I.ship.transform.position - transform.position);
                }

                targetFOV = normalFOV;
            } else if (state == CamState.ShipBack) //back
            {
                targetPosition = GameplayControl.I.ship.transform.position +
                    (Quaternion.Euler(new Vector3(-shipBackOffsetDegreePos, 0f, 0f)) * Vector3.up * shipBackDistance);
                if (Input.GetMouseButton(0))
                {
                    targetRotation = Quaternion.LookRotation(GameplayControl.I.ship.transform.position - transform.position) * Quaternion.Euler(Vector3.right * -shipBackOffsetDegrees);
                    targetFOV = shipBackOffsetFOV;
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(GameplayControl.I.ship.transform.position - transform.position);
                    targetFOV = normalFOV;
                }
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, fieldOfViewSpeed * Time.deltaTime);
    }
}
