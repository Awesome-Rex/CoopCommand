using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SceneControl), typeof(LevelGenerator))]
public class GameplayControl : SceneControl
{
    //set
    public static GameplayControl I;

    public List<FloorTrigger> floors;

    //generated
    public Vector3 goal;

    [Space]
    //dynamic
    public Controllable inControl;
    public float progress
    {
        get
        {
            return Vector3.Distance(ship.transform.position, goal) / (Vector3.Distance(Vector3.zero, transform.position) + Vector3.Distance(ship.transform.position, goal));
        }
    }


    [HideInInspector]
    public List<Usable> usables;
    [HideInInspector]
    public List<EnemySpawnPoint> spawnPoints;

    [Space]
    //reference
    public Transform ship;

    public void TransitionFloor (FloorTrigger floor)
    {
        int floorIndex = floors.IndexOf(floor);

        floor.Hide(false);

        for (int i = floorIndex+1; i < floors.Count; i++)
        {
            floors[i].Hide(true);
        }
    }

    protected override void Awake()
    {
        I = this;

        base.Awake();

        usables = new List<Usable>();
        foreach (Usable i in FindObjectsOfType<Usable>())
        {
            usables.Add(i);
        }

        spawnPoints = new List<EnemySpawnPoint>();
        foreach (EnemySpawnPoint i in FindObjectsOfType<EnemySpawnPoint>())
        {
            spawnPoints.Add(i);
        }
    }

    private void OnDrawGizmos()
    {
        if (I == null)
        {
            I = this;
        }
    }

#if UNITY_EDITOR
    //[CustomEditor<>]
    //public class E : Editor
    //{
    //    v
    //}
#endif
}
