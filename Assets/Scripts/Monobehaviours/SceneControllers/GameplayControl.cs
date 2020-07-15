using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SceneControl), typeof(LevelGenerator))]
public class GameplayControl : SceneControl
{
    //set
    public static GameplayControl I;

    //public List<FloorZone> floors;

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
    public FloorZone currentFloor;

    [HideInInspector]
    public List<Usable> usables;
    [HideInInspector]
    public List<EnemySpawnPoint> spawnPoints;

    [Space]
    //reference
    public Transform ship;

    //previous
    //private bool showAllFloorsPrevious;

    //public void TransitionFloor (FloorZone floor)
    //{
    //    int floorIndex = floors.IndexOf(floor);
    //    currentFloor = floor;

    //    if (!GameplayCamera.I.showAllFloors)
    //    {
    //        floor.hidden = false;
        
    //        for (int i = floorIndex + 1; i < floors.Count; i++)
    //        {
    //            floors[i].hidden = true;
    //        }
    //    }

    //    if (floor.name == "Attic")
    //    {
    //        MusicPlayer.I.gameMixer.FindSnapshot("Enclosed").TransitionTo(3f);
    //    }
    //    else if (floor.name == "Basement")
    //    {
    //        MusicPlayer.I.gameMixer.FindSnapshot("Enclosed").TransitionTo(3f);
    //    } else
    //    {
    //        MusicPlayer.I.gameMixer.FindSnapshot("Ground").TransitionTo(3f);
    //    }
    //}

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

        //load game
    }

    private void Update()
    {
        //if (GameplayCamera.I.showAllFloors && !showAllFloorsPrevious)
        //{
        //    foreach (FloorZone i in floors)
        //    {
        //        i.hidden = false;
        //    }
        //}
        //else if (!GameplayCamera.I.showAllFloors && showAllFloorsPrevious)
        //{
        //    TransitionFloor(currentFloor);
        //}

        //showAllFloorsPrevious = GameplayCamera.I.showAllFloors;
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
