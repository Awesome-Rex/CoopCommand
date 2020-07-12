using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameplayControl))]
public class LevelGenerator : MonoBehaviourPRO
{
    public float minDistance = 400f;
    public float maxDistance = 700f;

    private void Awake()
    {
        GetComponent<GameplayControl>().goal = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0f).normalized *
            Random.Range(minDistance, maxDistance);


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
