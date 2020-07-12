using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Walkable), typeof(User), typeof(Controllable))] //++ Health
public class Unit : MonoBehaviourPRO
{
    public DeadBody deadBody;

    private void Awake()
    {
        GetComponent<Health>().EDead += () =>
        {
            //instantiate dead version
            GameObject i = Instantiate(deadBody.gameObject);
            i.transform.position = transform.position;
            
            Destroy(this);
        };
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
