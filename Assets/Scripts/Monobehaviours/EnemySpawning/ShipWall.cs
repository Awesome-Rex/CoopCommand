using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ShipWall : MonoBehaviourPRO
{
    public List<EnemySpawnPoint> spawnPoints;

    private void Start()
    {
        foreach (EnemySpawnPoint i in spawnPoints)
        {
            i.enabled = false;
        }

        GetComponent<Health>().EDead += () =>
        {
            gameObject.SetActive(false);
            foreach (EnemySpawnPoint i in spawnPoints)
            {
                i.enabled = true;
            }
        };
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints.Count > 1) {
            Gizmos.color = Color.blue;

            for (int i = 0; i < spawnPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(spawnPoints[i].transform.position, spawnPoints[i + 1].transform.position);
            }

            Gizmos.DrawLine(spawnPoints[spawnPoints.Count - 1].transform.position, spawnPoints[0].transform.position);
        }
    }
}
