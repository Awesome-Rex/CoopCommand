using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class _ETERNAL : MonoBehaviour
{
    public static _ETERNAL I;

    [SerializeField]
    public GameControls controls;

    //children
    public bool transformableUsed;
    private Transform transformable;

    //even/odd frames
    public bool counter;


    //component references
    public ResourceReference resourceReference;

    public LateRecorder lateRecorder;
    public EarlyRecorder earlyRecorder;

    public void UseTransformable(Action<Transform> modifier)
    {
        if (!transformableUsed)
        {
            transformableUsed = true;

            transformable.transform.position = Vector3.zero;
            transformable.transform.eulerAngles = Vector3.zero;
            transformable.localScale = Vector3.one;

            modifier(transformable.transform);

            transformableUsed = false;
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        I = this;

        controls = new GameControls();

        //children
        transformableUsed = false;
        transformable = GameObject.FindGameObjectWithTag("Transformable").transform;

        counter = false;

        //component references
        resourceReference = GetComponent<ResourceReference>();

        lateRecorder = GetComponent<LateRecorder>();
        earlyRecorder = GetComponent<EarlyRecorder>();


        //settings
        lateRecorder.lateCallbackF += () => counter = !counter;
    }

    void OnEnable()
    {
        DontDestroyOnLoad(gameObject);

        I = this;

        controls = new GameControls();

        //children
        transformableUsed = false;
        transformable = GameObject.FindGameObjectWithTag("Transformable").transform;

        counter = false;

        //component references
        resourceReference = GetComponent<ResourceReference>();

        lateRecorder = GetComponent<LateRecorder>();
        earlyRecorder = GetComponent<EarlyRecorder>();


        //settings
        lateRecorder.lateCallbackF += () => counter = !counter;
    }
}
