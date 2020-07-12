using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameObjectTarget { Layer, Tag, Both }
public enum ColliderContact { Collider, Trigger, Both }

public class Damage : MonoBehaviour
{
    public float damage;

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
                HealthHitPoint comp = collision.gameObject.GetComponent<HealthHitPoint>();

                if (comp != null)
                {
                    comp.Damage(damage);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (contactType == ColliderContact.Trigger || contactType == ColliderContact.Both)
        {
            if (Qualified(other.gameObject))
            {
                HealthHitPoint comp = other.gameObject.GetComponent<HealthHitPoint>();

                if (comp != null)
                {
                    comp.Damage(damage);
                }
            }
        }
    }

    public bool Qualified (GameObject inspection)
    {
        if (targetType == GameObjectTarget.Layer)
        {
            return targetLayer == (targetLayer | (1 << inspection.layer));
        } else if (targetType == GameObjectTarget.Tag)
        {
            return inspection.tag == targetTag;
        } else
        {
            //both

            return targetLayer == (targetLayer | (1 << inspection.layer)) && inspection.tag == targetTag;
        }
    }
}
