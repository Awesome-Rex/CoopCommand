using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayViewCamera : MonoBehaviour
{
    public static GameplayViewCamera I;
    [HideInInspector]
    public Camera camera;

    private void Awake()
    {
        I = this;
        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        
    }
}
