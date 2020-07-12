using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGroup : MonoBehaviour
{
    public List<Health> group;

    public float health
    {
        get
        {
            float total = 0f;
            foreach (Health i in group)
            {
                total += i.health;
            }

            return total;
        }
    }
    public float maxHealth
    {
        get
        {
            float total = 0f;
            foreach (Health i in group)
            {
                total += i.maxHealth;
            }

            return total;
        }
    }

    public bool full
    {
        get
        {
            return health >= maxHealth;
        }
    }
    public bool dead
    {
        get
        {
            return health <= 0f;
        }
    }

    public System.Action EDead;

    private bool prevDead = false;

    [ContextMenu("Set Group")]
    public void SetGroup ()
    {
        group = new List<Health>();

        foreach (Health i in GetComponentsInChildren<Health>())
        {
            group.Add(i);
        }
    }

    void Update()
    {
        if (dead && !prevDead)
        {
            EDead();
        }

        prevDead = dead;
    }
}
