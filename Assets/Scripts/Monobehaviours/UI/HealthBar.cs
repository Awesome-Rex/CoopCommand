using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviourPRO
{
    public Health health;
    
    void Start()
    {
        GetComponent<Slider>().maxValue = health.maxHealth;
        GetComponent<Slider>().minValue = 0f;
    }

    void Update()
    {
        GetComponent<Slider>().value = health.health;
    }
}
