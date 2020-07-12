using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public float maxRadius = 2f;

    private void OnDrawGizmos()
    {
        if (enabled) {
            Gizmos.color = Color.yellow;
        } else
        {
            Gizmos.color = Color.grey;
        }
        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);

        Gizmos.DrawSphere(transform.position, maxRadius);
    }
}
