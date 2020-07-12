using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHitPoint : MonoBehaviour
{
    public Health affected;

    public void Damage (float value)
    {
        affected.health -= value;
    }
}
