using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;

    public float radiusRange = 0f;

    public Vector3 offset = Vector3.zero;
    public Vector3 direction = Vector3.forward;

    public void Shoot()
    {
        GameObject instance = Instantiate(bullet) as GameObject;
        bullet.transform.position = transform.TransformPoint(offset) + 
            (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(0f, radiusRange));

        bullet.transform.forward = (transform.TransformPoint(direction) - transform.position);
    }
}
