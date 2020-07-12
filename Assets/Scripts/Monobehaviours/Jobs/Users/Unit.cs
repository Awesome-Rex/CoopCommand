using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Walkable), typeof(User), typeof(Controllable))] //++ Health
public class Unit : MonoBehaviourPRO
{
    public Sprite deadBody;

    private void Awake()
    {
        GetComponent<Health>().EDead += () =>
        {
            //instantiate dead version
            GameObject i = Instantiate(ResourcesLoad<GameObject>("Prefabs/DeadBody"));
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
        if (GameplayControl.I.inControl.gameObject == gameObject)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + (Vector3.down * 0.25f), transform.forward, out hit, LayerMask.NameToLayer("Unit")))
            {
                //hit.collider.gameObject.GetComponent<Unit>().
            }
        }
    }
}
