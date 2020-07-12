using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RadiusSensor : MonoBehaviour
{
    public List<GameObject> targets;

    public ColliderContact contactType = ColliderContact.Collider;

    public GameObjectTarget targetType = GameObjectTarget.Layer;
    public LayerMask targetLayer;
    public string targetTag;

    public void OnCollisionEnter(Collision collision)
    {
        if (contactType == ColliderContact.Collider || contactType == ColliderContact.Both)
        {
            if (Qualified(collision.gameObject))
            {
                targets.Add(collision.gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (contactType == ColliderContact.Trigger || contactType == ColliderContact.Both)
        {
            if (Qualified(other.gameObject))
            {
                targets.Add(other.gameObject);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (contactType == ColliderContact.Collider || contactType == ColliderContact.Both)
        {
            if (Qualified(collision.gameObject))
            {
                targets.Remove(collision.gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (contactType == ColliderContact.Trigger || contactType == ColliderContact.Both)
        {
            if (Qualified(other.gameObject))
            {
                targets.Remove(other.gameObject);
            }
        }
    }

    public bool Qualified(GameObject inspection)
    {
        if (targetType == GameObjectTarget.Layer)
        {
            return targetLayer == (targetLayer | (1 << inspection.layer));
        }
        else if (targetType == GameObjectTarget.Tag)
        {
            return inspection.tag == targetTag;
        }
        else
        {
            return targetLayer == (targetLayer | (1 << inspection.layer)) && inspection.tag == targetTag;
        }
    }

    private void Awake()
    {
        targets = new List<GameObject>();
    }
}
