using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    public float maxHealth;

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

    // Update is called once per frame
    void Update()
    {
        if (full)
        {
            health = maxHealth;
        } else if (dead && !prevDead)
        {
            health = 0f;
            EDead();
        }

        prevDead = dead;
    }
}
