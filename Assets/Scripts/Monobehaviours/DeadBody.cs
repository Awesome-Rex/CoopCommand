using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(GameplayControl.I.ship.TransformPoint(
            new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            0f)).normalized * force, ForceMode.Impulse);

        Destroy(this, 6f);
    }
}
