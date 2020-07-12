using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Usable), typeof(RadiusSensor), typeof(Health))]
public class Turret : MonoBehaviourPRO
{
    public float minSeconds = 2f;
    public float maxSeconds = 6f;

    private bool running = false;

    IEnumerator shootEnemies ()
    {
        while (running)
        {
            //shoot nearest enemy

            if (GetComponent<RadiusSensor>().targets.Count > 0)
            {
                GameObject target = GetComponent<RadiusSensor>().targets[Random.Range(0, GetComponent<RadiusSensor>().targets.Count - 1)];

                transform.forward = target.transform.position - transform.position;

                //CREATE BULLET
            }

            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Usable>().used && !running)
        {
            running = true;
            StartCoroutine(shootEnemies());
        } else if (!GetComponent<Usable>().used && running)
        {
            running = false;
        }
    }
}
